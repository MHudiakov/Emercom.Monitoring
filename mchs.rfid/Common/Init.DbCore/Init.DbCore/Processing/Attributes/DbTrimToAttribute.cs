// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbTrimToAttribute.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Атрибут обрезки строки до указаной длины
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing.Attributes
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Атрибут обрезки строки до указаной длины
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class DbTrimToAttribute : ProcessAttribute
    {
        /// <summary>
        /// Атрибут обрезки строки до указаной длины
        /// </summary>
        /// <param name="length">Макисмальня длина строки</param>
        public DbTrimToAttribute(int length)
            : base(new Dictionary<string, object>() { { "Lenght", length } })
        {
            this.Lenght = length;
        }

        /// <summary>
        /// Макисмальня длина строки
        /// </summary>
        public int Lenght { get; private set; }
    }
}
