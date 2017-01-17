// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtDbAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Базовый класс внешнего атрибута БД
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Metadata
{
    using System;

    /// <summary>
    /// Базовый класс внешнего атрибута БД
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public abstract class ExtDbAttribute : DbAttribute
    {
        /// <summary>
        /// Имя свойства к которому применяется атрибут
        /// </summary>
        public string FieldName { get; private set; }

        /// <summary>
        /// Базовый класс внешнего атрибута БД
        /// </summary>
        /// <param name="fieldName">Имя свойства к которому применяется атрибут</param>
        protected ExtDbAttribute(string fieldName)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentNullException("fieldName");

            this.FieldName = fieldName;
        }
    }
}