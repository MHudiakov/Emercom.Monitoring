// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для массива объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System.Linq;

    /// <summary>
    /// Расширение для строкового преобразования массива объектов
    /// </summary>
    public static class CoreArrayExtension
    {
        /// <summary>
        /// Переводит массив в строку
        /// </summary>
        /// <param name="inputs">Массив объектов</param>
        /// <returns>Строка вида: []</returns>
        public static string ToFormatedString(this object[] inputs)
        {
            var inputString = "[" + ((ReferenceEquals(inputs, null) || inputs.Length == 0)
                                         ? "'null'"
                                         : inputs.Select(item => item != null ? "'" + item.ToString() + "'" : "'null'")
                                               .Aggregate((current, item) => current + ", " + item)) + "]";

            return inputString;
        }
    }
}