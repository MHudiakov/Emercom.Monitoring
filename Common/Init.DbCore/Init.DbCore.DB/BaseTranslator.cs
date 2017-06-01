// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseTranslator.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Базовый класс транслятора объектов из бд в шлюз записи
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB
{
    using System;
    using System.Data;

    using Init.DbCore.DB.Metadata;

    /// <summary>
    /// Базовый класс транслятора объектов из бд в шлюз записи
    /// </summary>
    /// <typeparam name="T">
    /// Тип объекта шлюза записи
    /// </typeparam>
    public class BaseTranslator<T>
        where T : DbObject, new()
    {
        /// <summary>
        /// Метеданные объекта
        /// </summary>
        protected virtual DbObjectSQLMetadata Metadata { get; private set; }

        /// <summary>
        /// Базовый класс транслятора объектов из бд в шлюз записи
        /// </summary>
        public BaseTranslator()
        {
            this.Metadata = new DbObjectSQLMetadata(typeof(T));
        }

        /// <summary>
        /// Создает объект из строки БД
        /// </summary>
        /// <param name="dr">
        /// Строка БД
        /// </param>
        /// <returns>
        /// Шлюз записи
        /// </returns>
        public virtual T CreateObjectFromDataRow(DataRow dr)
        {
            var item = new T();

            var fields = this.Metadata.DbFields;

            foreach (var prop in fields)
            {
                object dbValue = dr[prop.Value.DbFieldName];
                if (dbValue == null)
                    throw new InvalidOperationException(string.Format("Тип {0} содержит в объявлении контракта колонку: {1}, которой нет в БД.", typeof(T).FullName, prop.Value.DbFieldName));
                object netValue = this.ConvertToDotNetType(dbValue, prop.Key.PropertyInfo.PropertyType);
                prop.Key.Setter(item, netValue);
            }

            return item;
        }

        /// <summary>
        /// Конвертирует тип CLR в тип DB
        /// </summary>
        /// <param name="dotNetValue">
        /// CLR значение
        /// </param>
        /// <param name="toType">
        /// Тип в БД
        /// </param>
        /// <returns>
        /// Значение в БД
        /// </returns>
        public virtual object ConvertToDbType(object dotNetValue, Type toType)
        {
            var isNullable = Nullable.GetUnderlyingType(toType) != null;

            if (isNullable && dotNetValue == null)
                return DBNull.Value;

            // если тип nullable - получаем чистый тип
            if (isNullable)
                toType = Nullable.GetUnderlyingType(toType);

            if (toType == typeof(int))
                return TypeConverter.ToInt(dotNetValue);
            if (toType == typeof(string))
                return TypeConverter.ToString(dotNetValue);
            if (toType == typeof(double))
                return TypeConverter.ToDouble(dotNetValue);
            if (toType == typeof(decimal))
                return TypeConverter.ToDecimal(dotNetValue);

            if (toType == typeof(DateTime))
            {
                if (dotNetValue is int)
                    return DateTime.MinValue.AddSeconds((int)dotNetValue);
                return TypeConverter.ToDateTime(dotNetValue);
            }

            if (toType == typeof(TimeSpan))
            {
                if (dotNetValue is int)
                    return TimeSpan.FromSeconds((int)dotNetValue);
                return TypeConverter.ToTimeSpan(dotNetValue);
            }

            if (toType == typeof(bool))
                return TypeConverter.ToBoolean(dotNetValue);
            if (toType == typeof(long))
                return TypeConverter.ToLong(dotNetValue);
            if (toType == typeof(Guid))
                return dotNetValue;
            if (toType == typeof(ulong))
                return TypeConverter.ToULong(dotNetValue);

            throw new Exception(string.Format("Ошибка преобразования типа {0}=>{1}", dotNetValue == null ? "null" : dotNetValue.GetType().FullName, toType.FullName));
        }

        /// <summary>
        /// Ковертирует тип из БД в CLR
        /// </summary>
        /// <param name="dbValue">
        /// Значение из БД
        /// </param>
        /// <param name="toType">
        /// Целевой тип в CLR
        /// </param>
        /// <returns>
        /// Значение в CLR
        /// </returns>
        public virtual object ConvertToDotNetType(object dbValue, Type toType)
        {
            var isNullable = Nullable.GetUnderlyingType(toType) != null;

            if (isNullable && (dbValue == null || DBNull.Value == dbValue))
                return null;

            // если тип nullable - получаем чистый тип
            if (isNullable)
                toType = Nullable.GetUnderlyingType(toType);

            if (toType == typeof(int))
                return TypeConverter.ToInt(dbValue);
            if (toType == typeof(string))
                return TypeConverter.ToString(dbValue);
            if (toType == typeof(double))
                return TypeConverter.ToDouble(dbValue);
            if (toType == typeof(decimal))
                return TypeConverter.ToDecimal(dbValue);
            if (toType == typeof(DateTime))
            {
                if (dbValue is int)
                    return DateTime.MinValue.AddSeconds((int)dbValue);

                return TypeConverter.ToDateTime(dbValue);
            }

            if (toType == typeof(TimeSpan))
            {
                if (dbValue is int)
                    return TimeSpan.FromSeconds((int)dbValue);
                return TypeConverter.ToTimeSpan(dbValue);
            }

            if (toType == typeof(bool))
                return TypeConverter.ToBoolean(dbValue);
            if (toType == typeof(Version))
                return TypeConverter.ToVersion(dbValue);
            if (toType == typeof(long))
                return TypeConverter.ToLong(dbValue);
            if (toType == typeof(Guid))
                return (Guid)dbValue;
            if (toType == typeof(ulong))
                return TypeConverter.ToULong(dbValue);

            throw new Exception(string.Format("Ошибка преобразования типа {0}=>{1}", dbValue == null ? "DBNull" : dbValue.GetType().FullName, toType.FullName));
        }
    }
}