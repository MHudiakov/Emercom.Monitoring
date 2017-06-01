// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreRegKeyExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Предоставляет пользователю методы работы с ключом регистрации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.IO;

    using Microsoft.Win32;

    /// <summary>
    /// Предоставляет пользователю методы работы с ключом регистрации
    /// </summary>
    public static class CoreRegKeyExtension
    {
        /// <summary>
        /// Получение значения регистрационного ключа
        /// </summary>
        /// <param name="regDir">
        /// Директория
        /// </param>
        /// <param name="regKey">
        /// Регистрационный ключ
        /// </param>
        /// <returns>
        /// Значение ключа
        /// </returns>
        public static object GetRegKeyValue(string regDir, string regKey)
        {
            using (var readKey = Registry.CurrentUser.OpenSubKey(regDir))
            {
                return readKey == null ? null : readKey.GetValue(regKey);
            }
        }

        /// <summary>
        /// Показывает, существует ли ключ регистрации
        /// </summary>
        /// <param name="regDir">
        /// Директория
        /// </param>
        /// <param name="regKey">
        /// Ключ регистрации
        /// </param>
        /// <returns>
        /// Возвращает true, если ключ существует, иначе false
        /// </returns>
        public static bool IsRegKeyExist(string regDir, string regKey)
        {
            using (var readKey = Registry.CurrentUser.OpenSubKey(regDir))
            {
                if (readKey == null)
                    return false;
                if (readKey.GetValue(regKey) == null)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Установка значения регистрационного ключа
        /// </summary>
        /// <param name="regDir">
        /// Директория
        /// </param>
        /// <param name="regKey">
        /// Регистрационный ключ
        /// </param>
        /// <param name="value">
        /// Значение
        /// </param>
        /// <exception cref="ArgumentException">
        /// Возбуждается при невозможности записи ключа
        /// </exception>
        public static void SetRegKeyValue(string regDir, string regKey, string value)
        {
            using (var saveKey = Registry.CurrentUser.CreateSubKey(regDir))
            {
                if (saveKey == null)
                    throw new ArgumentException(string.Format("Не удалось записать ключ: {0}", Path.Combine(regDir, regKey)));
                saveKey.SetValue(regKey, value);
            }
        }
    }
}