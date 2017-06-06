// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsSqlDataAccess.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Реализация шлюза доступа к таблице MS SQL базы данных с произвольным типом ключа.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;

    using Init.DbCore.DataAccess;
    using Init.DbCore.DB;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Реализация шлюза доступа к таблице MS SQL базы данных с произвольным типом ключа.
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    public class MsSqlDataAccess<T> : DataAccess<T, MsSqlCoreDb>
        where T : DbObject, new()
    {
        /// <summary>
        /// Транслятор объектов
        /// </summary>
        public BaseTranslator<T> Translator { get; private set; }

        /// <summary>
        /// Имя таблицы в БД
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// Метаданные объекта T
        /// </summary>
        protected new DbObjectSQLMetadata Metadata { get; private set; }

        /// <summary>
        ///  Получение готовых параметров для работы с классовм DB
        /// </summary>
        /// <param name="item">Объяет для олучения значений параметров</param>
        /// <param name="columns">Справочник колонок для получения параметров</param>
        /// <returns>Массив SQL параметров</returns>
        protected virtual DbParameter[] GetDbParams(T item, IDictionary<DbPropertyInfo, DbMemberAttribute> columns)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (columns == null)
                throw new ArgumentNullException("columns");
            var paramList = new List<DbParameter>();

            foreach (var dbField in columns)
            {
                object netValue = dbField.Key.Getter(item);
                object value = this.Translator.ConvertToDbType(netValue, dbField.Value.FieldType);
                var par = new SqlParameter(dbField.Value.DbFieldName, value);
                paramList.Add(par);
            }

            return paramList.ToArray();
        }

        /// <summary>
        ///  Реализация шлюза доступа к таблице MS SQL базы данных
        /// </summary>
        /// <param name="getContext">Функция получения конекста доступа к БД</param>
        public MsSqlDataAccess(Func<MsSqlCoreDb> getContext)
            : this(getContext, new BaseTranslator<T>())
        {
        }

        /// <summary>
        ///  Реализация шлюза доступа к таблице MS SQL базы данных
        /// </summary>
        /// <param name="getContext">Функция получения конекста доступа к БД</param>
        /// <param name="translator">Преобразователь объекта</param>
        /// <param name="tableName">Имя таблицы (переопределяет имя, указанное в атрибуте DbTableMember)</param>
        public MsSqlDataAccess(Func<MsSqlCoreDb> getContext, BaseTranslator<T> translator, string tableName = "")
            : base(getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            if (translator == null)
                throw new ArgumentNullException("translator");

            this.Translator = translator;

            this.Metadata = new DbObjectSQLMetadata(typeof(T));

            // проверяем атрибут таблицы. Приоритет отдаем настройке репозитория
            this.TableName = this.Metadata.TableName;
            if (!string.IsNullOrWhiteSpace(tableName))
                this.TableName = tableName;

            if (string.IsNullOrWhiteSpace(this.TableName))
                throw new InvalidOperationException(string.Format("Не задано имя таблицы для объекта [{0}]", typeof(T)));
        }

        #region List

        /// <summary>
        /// Получение всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public override List<T> GetAll()
        {
            var dt = this.Context.ExecuteDataTable(string.Format("Select * From [{0}]", this.TableName));
            return dt.Rows.OfType<DataRow>().Select(r => this.Translator.CreateObjectFromDataRow(r)).ToList();
        }

        #endregion

        #region Count

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public override long GetCount()
        {
            string sqlCount = string.Format("SELECT COUNT(*) FROM [{0}]", this.TableName);
            return (long)this.Translator.ConvertToDotNetType(this.Context.ExecuteScalar(sqlCount), typeof(long));
        }

        #endregion

        #region Add
        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void AddOverride(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            try
            {
                // не редактируем автогенерируемые поля
                if (item.Identity == null)
                {
                    this.Context.InsertIntoTable(this.TableName, null, false, false, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
                }
                else
                {
                    object ident = this.Context.InsertIntoTable(this.TableName, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
                    var identClr = this.Translator.ConvertToDotNetType(ident, item.Identity.PropertyInfo.PropertyType);
                    item.Identity.Setter(item, identClr);
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new InvalidOperationException(@"Нарушение уникальности одного из полей", ex);
                if (ex.Number == 547)
                    throw new InvalidOperationException(@"Добавляемый объект ссылается на несуществующие данные", ex);
                if (ex.Number == 515)
                    throw new InvalidOperationException(@"Не указаны обязательные поля записи", ex);
                throw;
            }
        }

        #endregion

        #region IdentityAdd
        /// <summary>
        /// Добавление с указанным идентификатором
        /// </summary>
        /// <param name="item">
        /// Добавляемый объект
        /// </param>
        public virtual void IdentityAdd(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            try
            {
                this.Context.IdentityInsertIntoTable(this.TableName, this.GetDbParams(item, this.Metadata.DbFields));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new InvalidOperationException(@"Нарушение уникальности одного из полей", ex);
                if (ex.Number == 547)
                    throw new InvalidOperationException(@"Добавляемый объект ссылается на несуществующие данные", ex);
                if (ex.Number == 515)
                    throw new InvalidOperationException(@"Не указаны обязательные поля записи", ex);
            }
        }
        #endregion

        #region Edit
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(T item)
        {
            string keyFieldName = Metadata.DbFields[Metadata.Key].DbFieldName;
            try
            {
                this.Context.UpdateTable(this.TableName, new List<DbParameter> { new SqlParameter(keyFieldName, item.KeyValue) }, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new InvalidOperationException(@"Нарушение уникальности одного из полей", ex);
                if (ex.Number == 547)
                    throw new InvalidOperationException(@"Добавляемый объект ссылается на несуществующие данные", ex);
                if (ex.Number == 515)
                    throw new InvalidOperationException(@"Не указаны обязательные поля записи", ex);
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            if (whereArgs == null)
                throw new ArgumentNullException("whereArgs");

            // проверяем поля БД
            var dbWhereArgs = new Dictionary<string, object>();
            foreach (var pair in whereArgs)
            {
                var propertyInfo = this.Metadata.Properties[pair.Key];
                if (!this.Metadata.DbFields.ContainsKey(propertyInfo))
                    throw new ArgumentException(string.Format("Свойство {0} не отмечено атрибутом DbMember", propertyInfo.PropertyInfo.Name));

                dbWhereArgs.Add(this.Metadata.DbFields[propertyInfo].DbFieldName, pair.Value);
            }

            string sqlWhere = string.Empty;
            foreach (var pair in dbWhereArgs)
            {
                if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere += " AND ";
                sqlWhere += string.Format("[{0}]=@{0}", pair.Key);
            }

            try
            {
                this.Context.ExecuteScalar(string.Format("DELETE From [{0}] WHERE {1}", this.TableName, sqlWhere), dbWhereArgs.Select(e => (DbParameter)new SqlParameter(e.Key, e.Value)).ToArray());
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new InvalidOperationException(@"Нельзя узадить запись, потому что на нее ссылаются другие данные", ex);
                throw;
            }
        }
        #endregion

        #region Get
        /// <summary>
        /// Получение сиска объектов по ключу
        /// </summary>
        /// <param name="whereArgs">
        /// Ключ отбора объектов
        /// </param>
        /// <returns>
        /// Список объектов подходящий под критерий
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            if (whereArgs == null)
                throw new ArgumentNullException("whereArgs");

            // проверяем поля БД
            var dbWhereArgs = new Dictionary<string, object>();
            foreach (var pair in whereArgs)
            {
                var propertyInfo = this.Metadata.Properties[pair.Key];
                if (!this.Metadata.DbFields.ContainsKey(propertyInfo))
                    throw new ArgumentException(string.Format("Свойство {0} не отмечено атрибутом DbMember", propertyInfo.PropertyInfo.Name));

                dbWhereArgs.Add(this.Metadata.DbFields[propertyInfo].DbFieldName, pair.Value);
            }

            var items = new List<T>();

            string sqlWhere = string.Empty;
            foreach (var pair in dbWhereArgs)
            {
                if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere += " AND ";
                sqlWhere += string.Format(pair.Value == null ? "{0} is null" : "[{0}]=@{0}", pair.Key);
            }

            var dt = this.Context.ExecuteDataTable(string.Format("Select * From [{0}] WHERE {1}", this.TableName, sqlWhere), dbWhereArgs.Select(e => (DbParameter)new SqlParameter(e.Key, e.Value)).ToArray());

            items.AddRange(from DataRow row in dt.Rows select this.Translator.CreateObjectFromDataRow(row));

            return items;
        }

        #endregion

        #region GetPageOverride
        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="pageSize">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов со страницы pageIndex
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            string keyFieldName = Metadata.DbFields[Metadata.Key].DbFieldName;
            var sql = string.Format("Select TOP {0} * From [{1}] order by {2} WHERE {2} NOT IN (Select TOP {3} {2} From [{1}] order by {2})", pageSize, this.TableName, keyFieldName, pageSize * pageIndex);
            var dt = this.Context.ExecuteDataTable(sql);
            return dt.Rows.OfType<DataRow>().Select(r => Translator.CreateObjectFromDataRow(r)).ToList();
        }
        #endregion
    }
}