// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtProcessAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширенный атрибут для внешних объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Расширенный атрибут для внешних объектов
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public abstract class ExtProcessAttribute : ProcessAttribute
    {
        /// <summary>
        /// Расширенный атрибут для внешних объектов
        /// </summary>
        /// <param name="propertyName">Название поля к которому будет применяться обработка</param>
        /// <param name="args">Коллекция аргументов</param>
        protected ExtProcessAttribute(string propertyName, Dictionary<string, object> args)
            : base(args)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Название поля к которому будет применяться обработка
        /// </summary>
        public string PropertyName { get; private set; }
    }
}