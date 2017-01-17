// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbObjectMetadata.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Статичный Generiс класс для кеширования мапперов и
//   прочих статичных свойств DbObject
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;

    using Init.Tools;

    /// <summary>
    /// Статичный Generiс класс для кеширования мапперов и 
    /// прочих статичных свойств DbObject
    /// </summary>
    public class DbObjectMetadata
    {
        /// <summary>
        /// Свойство, отмеченное атрибутом <see cref="DbIdentityAttribute"/>
        /// </summary>
        public DbPropertyInfo Identity { get; private set; }

        /// <summary>
        /// Свойства, отмеченные атрубитом <see cref="DbKeyAttribute"/>
        /// </summary>
        public DbPropertyInfo Key { get; private set; }

        /// <summary>
        /// Свойства, отмеченные атрубитом <see cref="DataMemberAttribute"/>. Включают в себя коллекцию ключей
        /// </summary>
        public ReadOnlyDictionaryWrapper<string, DbPropertyInfo> Properties { get; private set; }

        /// <summary>
        /// Статичный Generiс класс для кеширования мапперов и 
        /// прочих статичных свойств DbObject
        /// </summary>
        /// <param name="dbObjType">
        /// Тип объекта Db
        /// </param>
        public DbObjectMetadata(Type dbObjType)
        {
            if (dbObjType == null)
                throw new ArgumentNullException("dbObjType");

            // получаем свойства помеченное DataMemberAttribute
            this.Properties = new ReadOnlyDictionaryWrapper<string, DbPropertyInfo>(dbObjType.GetProperties().Where(p => p.GetCustomAttributes(typeof(DataMemberAttribute), true).Any()).Select(p => new DbPropertyInfo(p, dbObjType)).ToDictionary(e => e.PropertyInfo.Name));

            // валидация внешних атрибутов
            var extDbAttributes = dbObjType.GetCustomAttributes(typeof(ExtDbAttribute), true).OfType<ExtDbAttribute>().ToList();
            string err = string.Empty;
            foreach (var attribute in extDbAttributes)
            {
                if (this.Properties.All(e => e.Value.PropertyInfo.Name != attribute.FieldName))
                    err += string.Format("Объект {0} содержит внешний атрибут, указывающий на несуществуюущее свойство: {1}\r\n", dbObjType.FullName, attribute.FieldName);
            }

            if (err != string.Empty)
                throw new ArgumentException(err, "dbObjType");

            // Заполняем ссылку на Identity
            if (this.Properties.Count(f => f.Value.IsIdentity) > 1)
                throw new ArgumentException("Шлюз записи не может иметь более одного поля отмеченного отрибутом DbIdentityAttribute");

            this.Identity = this.Properties.Select(e => e.Value).SingleOrDefault(f => f.IsIdentity);

            if (Properties.Count(p => p.Value.IsKey) > 1)
                throw new NotSupportedException(string.Format("Множественные ключи не поддерживаются. Тип [{0}]", dbObjType.FullName));

            // Заполняем ключи
            this.Key = this.Properties.SingleOrDefault(f => f.Value.IsKey).Value;
        }
    }
}