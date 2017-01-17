// --------------------------------------------------------------------------------------------------------------------
// <copyright company="ИНИТ-центр" file="CoreNameValueCollectionExtension.cs">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Расширение для NameValueCollection
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools 
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Расширение для NameValueCollection
    /// </summary>
    public static class CoreNameValueCollectionExtension
    {
        /// <summary>
        /// Читает булевое значение
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Прочитанное значение или значение по умолчанию</returns>
        public static bool GetBool(this NameValueCollection collection, string key, bool defaultValue)
        {
            bool result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
                bool.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Читает булевое значение
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <returns>Прочитанное значение или false</returns>
        public static bool GetBool(this NameValueCollection collection, string key) 
        {
            return GetBool(collection, key, false);
        }

        /// <summary>
        /// Читает дату
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Прочитанное значение или defaultValue</returns>
        public static DateTime GetDate(this NameValueCollection collection, string key, DateTime defaultValue) 
        {
            DateTime result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
                DateTime.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Читает дату
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <returns>Прочитанное значение или DateTime.MinValue</returns>
        public static DateTime GetDate(this NameValueCollection collection, string key) 
        {
            return GetDate(collection, key, DateTime.MinValue);
        }

        /// <summary>
        /// Читает Enum
        /// </summary>
        /// <typeparam name="T">
        /// Тип Enum
        /// </typeparam>
        /// <param name="collection">
        /// Коллекция
        /// </param>
        /// <param name="key">
        /// Ключ
        /// </param>
        /// <param name="defaultValue">
        /// Значение по умолчанию
        /// </param>
        /// <returns>
        /// Прочитанное значение или defaultValue
        /// </returns>
        public static T GetEnum<T>(this NameValueCollection collection, string key, T defaultValue) 
        {
            T result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
                result = (T)Enum.Parse(typeof(T), value, true);
            return result;
        }

        /// <summary>
        /// Читает enum.
        /// </summary>
        /// <typeparam name="T">
        /// Тип Enum
        /// </typeparam>
        /// <param name="collection">
        /// Коллекция
        /// </param>
        /// <param name="key">
        /// Ключ
        /// </param>
        /// <returns>
        /// Прочитанное значение или default(T)
        /// </returns>
        public static T GetEnum<T>(this NameValueCollection collection, string key) 
        {
            return GetEnum(collection, key, default(T));
        }

        /// <summary>
        /// Читает int.
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Прочитанное значение или defaultValue</returns>
        public static int GetInt(this NameValueCollection collection, string key, int defaultValue) 
        {
            int result = defaultValue;
            string value = collection[key];
            if (!string.IsNullOrEmpty(value))
                int.TryParse(value, out result);
            return result;
        }

        /// <summary>
        /// Gets the int.
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <returns>Прочитанное значение или 0</returns>
        public static int GetInt(this NameValueCollection collection, string key) 
        {
            return GetInt(collection, key, 0);
        }

        /// <summary>
        /// Читает string.
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Прочитанное значение или defaultValue</returns>
        public static string GetString(this NameValueCollection collection, string key, string defaultValue) 
        {
            return collection[key] ?? defaultValue;
        }

        /// <summary>
        /// Читает string.
        /// </summary>
        /// <param name="collection">Коллекция</param>
        /// <param name="key">Ключ</param>
        /// <returns>Прочитанное значение или null</returns>
        public static string GetString(this NameValueCollection collection, string key) 
        {
            return GetString(collection, key, null);
        }
    }
}
