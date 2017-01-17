// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbMemberAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Этим атрибутом отмечаются поля, которые нужно сохранятьв БД
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.Metadata
{
    using System;

    /// <summary>
    /// Этим атрибутом отмечаются поля, которые нужно сохранять в БД
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DbMemberAttribute : Attribute
    {
        /// <summary>
        /// Этим атрибутом отмечаются поля, которые нужно сохранятьв БД
        /// </summary>
        /// <param name="dbField">Название колонки в БД</param>
        /// <param name="dbFieldType">Тип колонки в БД</param>
        public DbMemberAttribute(string dbField, Type dbFieldType)
        {
            this.DbFieldName = dbField;
            this.FieldType = dbFieldType;
        }

        /// <summary>
        /// Этим атрибутом отмечаются поля, которые нужно сохранятьв БД
        /// (Имя колонки в БД совпадает с именем свойства)
        /// </summary>
        /// <param name="dbFieldType">Тип колонки в БД</param>
        public DbMemberAttribute(Type dbFieldType)
        {
            this.FieldType = dbFieldType;
        }

        /// <summary>
        /// Этим атрибутом отмечаются поля, которые нужно сохранятьв БД
        /// (Имя и тип колонки в БД совпадают с именем и типом свойства)
        /// </summary>
        public DbMemberAttribute()
        {
        }

        /// <summary>
        /// Название колонкив БД
        /// </summary>
        public string DbFieldName { get; private set; }

        /// <summary>
        /// Тип колонки в БД
        /// </summary>
        public Type FieldType { get; private set; }
    }
}