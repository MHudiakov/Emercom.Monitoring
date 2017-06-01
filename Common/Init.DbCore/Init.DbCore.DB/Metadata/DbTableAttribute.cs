// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbTableAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Defines the DbTableAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.Metadata
{
    using System;

    /// <summary>
    /// Этим атрибутом можно указать в какой таблице хранятся шлюзы записей
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class DbTableAttribute : Attribute
    {
        /// <summary>
        /// Этим атрибутом можно указать в какой таблице хранятся шлюзы записей
        /// </summary>
        /// <param name="tableName">Имя таблицы в БД, в котрой хранится объект</param>
        public DbTableAttribute(string tableName)
        {
            this.TableName = tableName;
        }

        /// <summary>
        /// Этим атрибутом можно указать в какой таблице хранятся шлюзы записей
        /// (Имя таблицы в БД совпадает с именем объекта)
        /// </summary>
        public DbTableAttribute()
        {
        }

        /// <summary>
        /// Имя таблицы в БД, в котрой хранится объект
        /// </summary>
        public string TableName { get; private set; }
    }
}