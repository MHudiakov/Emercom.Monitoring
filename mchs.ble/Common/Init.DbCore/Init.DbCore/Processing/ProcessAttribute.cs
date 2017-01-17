// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый атрибут валидации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Базовый атрибут валидации
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    public abstract class ProcessAttribute : Attribute
    {
        /// <summary>
        /// Аргументы для передачи стратегии
        /// </summary>
        public Dictionary<string, object> Args { get; private set; }

        /// <summary>
        /// Базовый атрибут валидации
        /// </summary>
        /// <param name="args">Аргументы для передачи стратегии</param>
        protected ProcessAttribute(Dictionary<string, object> args)
        {
            if (args == null)
                throw new ArgumentNullException("args");

            this.Args = args;
        }
    }
}
