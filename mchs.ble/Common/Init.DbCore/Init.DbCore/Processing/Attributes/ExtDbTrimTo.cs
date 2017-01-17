// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtDbTrimTo.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Расширенный атрибут по заданию максимальной длянны строки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Processing.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Расширенный атрибут по заданию максимальной длянны строки
    /// </summary>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ExtDbTrimTo : ExtProcessAttribute
    {
        /// <summary>
        /// Расширенный атрибут по заданию максимальной длянны строки
        /// </summary>
        /// <param name="propertyName">Название поля</param>
        /// <param name="lenght">Длинна до которой "обрезается" строка</param>
        public ExtDbTrimTo(string propertyName, int lenght)
            : base(propertyName, new Dictionary<string, object>() { { "Lenght", lenght } })
        {
            this.Lenght = lenght;
        }

        /// <summary>
        /// Длинна строки
        /// </summary>
        public int Lenght { get; private set; }
    }
}
