// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreByteArrayHelper.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для байтовоно массива
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;

    /// <summary>
    /// Расширение для байтовоно массива
    /// </summary>
    public static class CoreByteArrayHelper
    {
        /// <summary>
        /// Преобразует массив в HEX строку
        /// </summary>
        /// <param name="arr">Массив байт</param>
        /// <returns>HEX строка без разделителей</returns>
        public static string ToHexString(this byte[] arr)
        {
            return BitConverter.ToString(arr).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// Преобразует HEX строку в массив байт
        /// </summary>
        /// <param name="hex">HEX строка без разделителей</param>
        /// <returns>Массив байт</returns>
        public static byte[] HexToByteArray(this string hex)
        {
            int numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
