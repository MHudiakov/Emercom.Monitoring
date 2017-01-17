// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeConverter.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Конвертор типов данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Init.Tools;

    /// <summary>
    /// Конвертор типов данных
    /// </summary>
    public static class TypeConverter
    {
        // ReSharper disable InvokeAsExtensionMethod
        #region Simple
        /// <summary>
        /// Преобразовать к Int
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static int ToInt(object from, int def = 0)
        {
            return CoreConvertExtension.ToInt(from, def);
        }

        /// <summary>
        /// Преобразовать к double
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static double ToDouble(object from, double def = 0)
        {
            return CoreConvertExtension.ToDouble(from, def);
        }

        /// <summary>
        /// Преобразовать к decimal
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static decimal ToDecimal(object from, decimal def = 0)
        {
            return CoreConvertExtension.ToDecimal(from, def);
        }

        /// <summary>
        /// Преобразовать к DateTime
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static DateTime ToDateTime(object from)
        {
            return CoreConvertExtension.ToDateTime(from, DateTime.MinValue);
        }

        /// <summary>
        /// Преобразовать к DateTime
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static DateTime ToDateTime(object from, DateTime def)
        {
            return CoreConvertExtension.ToDateTime(from, def);
        }

        /// <summary>
        /// Преобразовать к TimeSpan
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static TimeSpan ToTimeSpan(object from)
        {
            return CoreConvertExtension.ToTime(from, TimeSpan.Zero);
        }

        /// <summary>
        /// Преобразовать к TimeSpan
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static TimeSpan ToTimeSpan(object from, TimeSpan def)
        {
            return CoreConvertExtension.ToTime(from, def);
        }

        /// <summary>
        /// Преобразовать к bool
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static bool ToBoolean(object from, bool def = false)
        {
            return CoreConvertExtension.ToBoolean(from, def);
        }

        /// <summary>
        /// Преобразовать к string
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static string ToString(object from)
        {
            return CoreConvertExtension.ToString(from);
        }

        /// <summary>
        /// Преобразовать к Version
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static Version ToVersion(object from)
        {
            return CoreConvertExtension.ToVersion(from);
        }

        /// <summary>
        /// Преобразовать к long
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static long ToLong(object from, long def = 0)
        {
            return CoreConvertExtension.ToLong(from, def);
        }

        /// <summary>
        /// Преобразовать к ulong
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <param name="def">
        /// Значение по умолчанию, если ну удалось преобразовать
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP0100:AdvancedNamingRules", Justification = "Reviewed. Suppression is OK here.")]
        public static ulong ToULong(object from, ulong def = 0)
        {
            return CoreConvertExtension.ToULong(from, def);
        }

        // ReSharper restore InvokeAsExtensionMethod
        #endregion
    }
}