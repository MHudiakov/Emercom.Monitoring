// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbPropertyInfo.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Описывает поле, управляемое DbCore
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Init.Tools;

    /// <summary>
    /// Описывает поле, управляемое DbCore (Отмечены атрибутом DataMember)
    /// </summary>
    public class DbPropertyInfo : PropertyHelper<DbObject>
    {
        /// <summary>
        /// Описывает поле БД
        /// </summary>
        /// <param name="propertyInfo">Свойство в CLR объекте, соответствуюущее колонке в БД</param>
        /// <param name="ownerType">Тип объекта-владельца поля</param>
        public DbPropertyInfo(PropertyInfo propertyInfo, Type ownerType)
            : base(propertyInfo)
        {
            // проверяем автогенерируемое поле (DbIdentityAttribute)

            // если DbIdentity установлено извне
            var extIdent = ownerType.GetCustomAttributes(true).OfType<ExtDbIdentityAttribute>().SingleOrDefault();
            if (extIdent != null && extIdent.FieldName == propertyInfo.Name)
                this.IsIdentity = true;

            // проверяем атрибуты самого поля
            if (!this.IsIdentity)
                this.IsIdentity = propertyInfo.GetCustomAttributes(typeof(DbIdentityAttribute), true).Any();

            if (this.IsIdentity && !propertyInfo.PropertyType.IsValueType)
                throw new InvalidOperationException("Атрибутом DbIdentityAttribute можно отмечать только поля Value типа");

            // проверяем ключевое поле (DbKeyAttribute)

            // если DbKey установлено извне
            var extKeys = ownerType.GetCustomAttributes(true).OfType<ExtDbKeyAttribute>().ToList();
            this.IsKey = extKeys.Exists(k => k.FieldName == propertyInfo.Name);

            // проверяем атрибуты самого поля
            if (!this.IsKey)
                this.IsKey = propertyInfo.GetCustomAttributes(typeof(DbKeyAttribute), true).Any();

            if (this.IsKey && !propertyInfo.PropertyType.IsValueType)
                throw new InvalidOperationException("Атрибутом DbKeyAttribute можно отмечать только Value типы");
        }

        /// <summary>
        /// Флаг: поля является ключем
        /// </summary>
        public bool IsKey { get; private set; }

        /// <summary>
        /// Флаг: поле является идентификатором
        /// </summary>
        public bool IsIdentity { get; private set; }
    }
}