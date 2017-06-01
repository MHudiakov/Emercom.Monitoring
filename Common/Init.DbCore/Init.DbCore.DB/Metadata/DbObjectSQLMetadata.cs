// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbObjectSQLMetadata.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Утилита, предоставляет вспомогательные функции.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.Metadata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore.Metadata;
    using Init.Tools;

    /// <summary>
    /// Метеданные объекта БД, включающие описание полей SQL
    /// </summary>
    public class DbObjectSQLMetadata : DbObjectMetadata
    {
        /// <summary>
        /// Метеданные объекта БД
        /// </summary>
        /// <param name="dbObjType">Тип объекта</param>
        public DbObjectSQLMetadata(Type dbObjType)
            : base(dbObjType)
        {
            if (dbObjType == null)
                throw new ArgumentNullException("dbObjType");

            // Определяем имя таблицы для данного объекта
            var tableAttr = dbObjType.GetCustomAttributes(true).OfType<DbTableAttribute>().LastOrDefault();
            if (tableAttr != null)
                this.TableName = tableAttr.TableName ?? dbObjType.Name;

            // находим все свойства, отмеченный атрибутом DbMemberAttribute
            var dbFields =
                dbObjType.GetProperties()
                         .Where(p => p.CanRead && p.CanWrite)
                         .Where(p => p.GetCustomAttributes(true).OfType<DbMemberAttribute>().Any())
                         .Select(p => new DbPropertyInfo(p, dbObjType))
                         .ToList();

            // Составляем пары свойство-атрибут
            var dict = new Dictionary<DbPropertyInfo, DbMemberAttribute>();
            foreach (var item in dbFields)
            {
                var attr = (DbMemberAttribute)item.PropertyInfo.GetCustomAttributes(typeof(DbMemberAttribute), true).SingleOrDefault();

                if (attr == null)
                    throw new Exception(
                        string.Format(
                            "Не удалось получить атрибут [DbMember] свойства {0} объекта {1}",
                            item.PropertyInfo.Name,
                            dbObjType.FullName));

                if (attr.DbFieldName == null)
                {
                    if (attr.FieldType != null)
                    {
                        var propType = attr.FieldType;
                        attr = new DbMemberAttribute(item.PropertyInfo.Name, propType);
                    }
                    else
                        attr = new DbMemberAttribute(item.PropertyInfo.Name, item.PropertyInfo.PropertyType);
                }

                dict.Add(item, attr);
            }

            this.DbFields = new ReadOnlyDictionaryWrapper<DbPropertyInfo, DbMemberAttribute>(dict);
            this.DbFieldsWithoutIdent = new ReadOnlyDictionaryWrapper<DbPropertyInfo, DbMemberAttribute>(dict.Where(e => !e.Key.IsIdentity).ToDictionary(e => e.Key, e => e.Value));
        }

        /// <summary>
        /// Перечень полей отображаемых на базу данных
        /// </summary>
        public ReadOnlyDictionaryWrapper<DbPropertyInfo, DbMemberAttribute> DbFields { get; private set; }

        /// <summary>
        /// Перечень полей(За исключением Identity) отображаемых на базу данных
        /// </summary>
        public ReadOnlyDictionaryWrapper<DbPropertyInfo, DbMemberAttribute> DbFieldsWithoutIdent { get; private set; }

        /// <summary>
        /// Имя таблицы, указанное в атрибуте
        /// </summary>
        public string TableName { get; private set; }
    }
}