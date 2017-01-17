// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MySqlDbDataAccess.cs" company="����-�����">
//   ����-�����, 2014�.
// </copyright>
// <summary>
//   ���������� ����� ������� � ������� MS SQL ���� ������ � ������������ ����� �����.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.MySql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    using Init.DbCore.DataAccess;
    using Init.DbCore.DB;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    using global::MySql.Data.MySqlClient;

    /// <summary>
    /// ���������� ����� ������� � ������� MS SQL ���� ������ � ������������ ����� �����.
    /// </summary>
    /// <typeparam name="T">
    /// ��� ����� ������
    /// </typeparam>
    public class MySqlDbDataAccess<T> : DataAccess<T, MySqlCoreDb>
        where T : DbObject, new()
    {
        /// <summary>
        /// ���������� ��������
        /// </summary>
        public BaseTranslator<T> Translator { get; private set; }

        /// <summary>
        /// ��� ������� � ��
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// ���������� ������� T
        /// </summary>
        protected new DbObjectSQLMetadata Metadata { get; private set; }

        /// <summary>
        ///  ��������� ������� ���������� ��� ������ � �������� DB
        /// </summary>
        /// <param name="item">������ ��� �������� �������� ����������</param>
        /// <param name="columns">���������� ������� ��� ��������� ����������</param>
        /// <returns>������ SQL ����������</returns>
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
                var par = new MySqlParameter(dbField.Value.DbFieldName, value);
                paramList.Add(par);
            }

            return paramList.ToArray();
        }

        /// <summary>
        ///  ���������� ����� ������� � ������� MySQL ���� ������
        /// </summary>
        /// <param name="getContext">������� ��������� �������� ������� � ��</param>
        public MySqlDbDataAccess(Func<MySqlCoreDb> getContext)
            : this(getContext, new BaseTranslator<T>())
        {
        }

        /// <summary>
        ///  ���������� ����� ������� � ������� MySQL ���� ������
        /// </summary>
        /// <param name="getContext">������� ��������� �������� ������� � ��</param>
        /// <param name="translator">��������������� �������</param>
        /// <param name="tableName">��� ������� (�������������� ���, ��������� � �������� DbTableMember)</param>
        public MySqlDbDataAccess(Func<MySqlCoreDb> getContext, BaseTranslator<T> translator, string tableName = "")
            : base(getContext)
        {
            if (getContext == null)
                throw new ArgumentNullException("getContext");
            if (translator == null)
                throw new ArgumentNullException("translator");

            this.Translator = translator;

            this.Metadata = new DbObjectSQLMetadata(typeof(T));

            // ��������� ������� �������. ��������� ������ ��������� �����������
            this.TableName = this.Metadata.TableName;
            if (!string.IsNullOrWhiteSpace(tableName))
                this.TableName = tableName;

            if (string.IsNullOrWhiteSpace(this.TableName))
                throw new InvalidOperationException(string.Format("�� ������ ��� ������� ��� ������� [{0}]", typeof(T)));
        }

        #region List
        /// <summary>
        /// ��������� ���� ��������
        /// </summary>
        /// <returns>
        /// ������ ���� �������� � �������
        /// </returns>
        public override List<T> GetAll()
        {
            var dt = this.Context.ExecuteDataTable(string.Format("Select * From {0}", this.TableName));
            return dt.Rows.OfType<DataRow>().Select(r => this.Translator.CreateObjectFromDataRow(r)).ToList();
        }
        #endregion

        #region Count
        /// <summary>
        /// ��������� ���������� ��������
        /// </summary>
        /// <returns>
        /// ���������� �������� � ��
        /// </returns>
        public override long GetCount()
        {
            string sqlCount = string.Format("SELECT COUNT(*) FROM {0}", this.TableName);
            return (long)this.Translator.ConvertToDotNetType(this.Context.ExecuteScalar(sqlCount), typeof(long));
        }
        #endregion

        #region Add
        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected override void AddOverride(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            // �� ����������� ���������������� ����
            if (item.Identity == null)
                this.Context.InsertIntoTable(this.TableName, null, false, false, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
            else
            {
                object ident = this.Context.InsertIntoTable(this.TableName, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
                var identClr = this.Translator.ConvertToDotNetType(ident, item.Identity.PropertyInfo.PropertyType);
                item.Identity.Setter(item, identClr);
            }
        }
        #endregion

        #region IdentityAdd
        /// <summary>
        /// ���������� � ��������� ���������������
        /// </summary>
        /// <param name="item">
        /// ����������� ������
        /// </param>
        public virtual void IdentityAdd(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            this.Context.IdentityInsertIntoTable(this.TableName, this.GetDbParams(item, this.Metadata.DbFields));
        }
        #endregion

        #region Edit
        /// <summary>
        /// ���������� �������
        /// </summary>
        /// <param name="item">������</param>
        protected override void EditOverride(T item)
        {
            // ����� �������
            string keyFieldName = Metadata.DbFields[Metadata.Key].DbFieldName;

            // �� ����������� ���������������� ����
            this.Context.UpdateTable(this.TableName, new List<DbParameter> { new MySqlParameter(keyFieldName, item.KeyValue) }, this.GetDbParams(item, this.Metadata.DbFieldsWithoutIdent));
        }
        #endregion

        #region Delete
        /// <summary>
        /// �������� �������
        /// </summary>
        /// <param name="whereArgs">
        /// ����� �������
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            if (whereArgs == null)
                throw new ArgumentNullException("whereArgs");

            // ��������� ���� ��
            var dbWhereArgs = new Dictionary<string, object>();
            foreach (var pair in whereArgs)
            {
                var propertyInfo = this.Metadata.Properties[pair.Key];
                if (!this.Metadata.DbFields.ContainsKey(propertyInfo))
                    throw new ArgumentException(string.Format("�������� {0} �� �������� ��������� DbMember", propertyInfo.PropertyInfo.Name));

                dbWhereArgs.Add(this.Metadata.DbFields[propertyInfo].DbFieldName, pair.Value);
            }

            string sqlWhere = string.Empty;
            foreach (var pair in dbWhereArgs)
            {
                if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere += " AND ";
                sqlWhere += string.Format("{0}=@{0}", pair.Key);
            }

            this.Context.ExecuteScalar(string.Format("DELETE From {0} WHERE {1}", this.TableName, sqlWhere), dbWhereArgs.Select(e => (DbParameter)new MySqlParameter(e.Key, e.Value)).ToArray());
        }
        #endregion

        #region Get
        /// <summary>
        /// ��������� ����� �������� �� �����
        /// </summary>
        /// <param name="whereArgs">
        /// ���� ������ ��������
        /// </param>
        /// <returns>
        /// ������ �������� ���������� ��� ��������
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            if (whereArgs == null)
                throw new ArgumentNullException("whereArgs");

            // ��������� ���� ��
            var dbWhereArgs = new Dictionary<string, object>();
            foreach (var pair in whereArgs)
            {
                var propertyInfo = this.Metadata.Properties[pair.Key];
                if (!this.Metadata.DbFields.ContainsKey(propertyInfo))
                    throw new ArgumentException(string.Format("�������� {0} �� �������� ��������� DbMember", propertyInfo.PropertyInfo.Name));

                dbWhereArgs.Add(this.Metadata.DbFields[propertyInfo].DbFieldName, pair.Value);
            }

            var items = new List<T>();

            string sqlWhere = string.Empty;
            foreach (var pair in dbWhereArgs)
            {
                if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere += " AND ";
                sqlWhere += string.Format("{0}=@{0}", pair.Key);
            }

            var dt = this.Context.ExecuteDataTable(string.Format("Select * From {0} WHERE {1}", this.TableName, sqlWhere), dbWhereArgs.Select(e => (DbParameter)new MySqlParameter(e.Key, e.Value)).ToArray());

            items.AddRange(from DataRow row in dt.Rows select this.Translator.CreateObjectFromDataRow(row));

            return items;
        }
        #endregion

        #region GetPageOverride
        /// <summary>
        /// ��������� ����� ��������
        /// </summary>
        /// <param name="pageIndex">
        /// ����� ��������
        /// </param>
        /// <param name="pageSize">
        /// ���������� ��������
        /// </param>
        /// <returns>
        /// ������ �������� �� �������� pageIndex
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            string keyFieldName = Metadata.DbFields[Metadata.Key].DbFieldName;
            var sql = string.Format("Select * From {1} order by {2} WHERE {2} NOT IN (Select {2} From {1} order by {2} Limit {3}) Limit {0}", pageSize, this.TableName, keyFieldName, pageSize * pageIndex);
            var dt = this.Context.ExecuteDataTable(sql);
            return dt.Rows.OfType<DataRow>().Select(r => Translator.CreateObjectFromDataRow(r)).ToList();
        }
        #endregion
    }
}