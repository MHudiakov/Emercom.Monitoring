// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreConvertExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Предоставляет пользователю методы конвертирования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    /// <summary>
    /// Предоставляет пользователю методы конвертирования 
    /// </summary>
    public static class CoreConvertExtension
    {
        /// <summary>
        /// Преобразовать к Version
        /// </summary>
        /// <param name="from">
        /// Исходный объект
        /// </param>
        /// <returns>
        /// Преобразованное значение
        /// </returns>
        public static Version ToVersion(this object from)
        {
            if (from == null)
                throw new ArgumentNullException("from");
            return new Version(from.ToString());
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
            return @from == null ? string.Empty : @from.ToString();
        }

        /// <summary>
        /// Приведение к nullable DataTime
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="DateTime"/>, или null, если переданный объект не является <see cref="DateTime"/>, 
        /// или является наименьшей датой типа <see cref="DateTime"/>
        /// </returns>
        public static DateTime? ToNullDateTime(this object obj)
        {
            if (obj == null)
                return null;
            var dt = ToDateTime(obj, DateTime.MinValue);
            if (dt == DateTime.MinValue)
                return null;
            return dt;
        }

        /// <summary>
        /// Приведение к формату дата/время
        /// </summary>
        /// <param name="obj">
        /// Привдимый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="DateTime"/> если принятый объект является <see cref="DateTime"/>,
        /// иначе возвращается наименьшее значение типа <see cref="DateTime"/>
        /// </returns>
        public static DateTime ToDateTime(this object obj)
        {
            return ToDateTime(obj, DateTime.MinValue);
        }

        /// <summary>
        /// Приведение к формату дата/время
        /// </summary>
        /// <param name="obj">
        /// Привдимый объект
        /// </param>
        /// <param name="defaultValue">
        /// Значение даты и времени по умолчанию
        /// </param>
        /// <returns>
        /// Возвращает <see cref="DateTime"/> если принятый объект является <see cref="DateTime"/>,
        /// иначе возвращается значение по умолчанию 
        /// </returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            if (obj == null)
                return defaultValue;

            if (obj is DateTime)
                return (DateTime)obj;

            DateTime result;
            if (DateTime.TryParse(obj.ToString(), out result))
                return result;
            return defaultValue;
        }

        /// <summary>
        /// Усечение времени до часов
        /// </summary>
        /// <param name="time">
        /// Дата/время
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// Полное представление даты с точностью до минут
        /// </returns>
        public static DateTime TrimMinutes(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
        }

        /// <summary>
        /// Усечение времени до минут
        /// </summary>
        /// <param name="time">
        /// Время
        /// </param>
        /// <returns>
        /// Полное представление даты с точностью до секунд
        /// </returns>
        public static DateTime TrimSeconds(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
        }

        /// <summary>
        /// Усечение времени до секунд
        /// </summary>
        /// <param name="time">
        /// Время
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime TrimMiliseconds(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
        }

        /// <summary>
        /// Определение, находится ли текщее значение даты/времени в заданном промежутке 
        /// (начальное значение промежутка не должно быть больше конечного)
        /// </summary>
        /// <param name="current">
        /// Значение даты/времени
        /// </param>
        /// <param name="fromDataTime">
        /// Начальное значение промежутка
        /// </param>
        /// <param name="toDataTime">
        /// Конечное значение промежутка
        /// </param>
        /// <returns>
        /// Возвращается true, если current находится между fromDataTime и toDataTime, иначе false
        /// </returns>
        public static bool IsBetween(this DateTime current, DateTime fromDataTime, DateTime toDataTime)
        {
            if (fromDataTime > toDataTime)
                throw new ArgumentException("Начальное значение промежутка больше конечного");
            return fromDataTime < toDataTime && current >= fromDataTime && current <= toDataTime;
        }

        /// <summary>
        /// Приведение к формату времени
        /// </summary>
        /// <param name="obj">
        /// Приводимый объект
        /// </param>
        /// <returns>
        /// The <see cref="TimeSpan"/>.
        /// </returns>
        public static TimeSpan ToTime(this object obj)
        {
            return ToTime(obj, TimeSpan.Zero);
        }

        /// <summary>
        /// Приведение к формату времени
        /// </summary>
        /// <param name="obj">
        /// Приводимый объект
        /// </param>
        /// <param name="defaultValue">
        /// Значение по умолчанию
        /// </param>
        /// <returns>
        /// Возвращается время дня, в случае ошибки возвращается defaultValue
        /// </returns>
        public static TimeSpan ToTime(this object obj, TimeSpan defaultValue)
        {
            if (obj == null)
                return defaultValue;
            try
            {
                var temp = obj as TimeSpan?;
                if (temp != null)
                    return (TimeSpan)temp;
                return Convert.ToDateTime(obj).TimeOfDay;
            }
            catch (FormatException)
            {
                return defaultValue;
            }
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
            try
            {
                if (from == null)
                    return def;

                if (from is DBNull)
                    return def;

                if (from is bool)
                    return (bool)from ? 1 : 0;

                return Convert.ToInt64(from);
            }
            catch (Exception)
            {
                return def;
            }
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
            try
            {
                if (from == null)
                    return def;

                if (from is DBNull)
                    return def;

                if (from is bool)
                    return ((bool)from) ? 1u : 0u;

                return Convert.ToUInt64(from);
            }
            catch (Exception)
            {
                return def;
            }
        }

        /// <summary>
        /// Преобразование к целочисленному типу
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="int"/>, в случае ошибки приведения возвращается 0
        /// </returns>
        public static int ToInt(this object obj)
        {
            return obj.ToInt(0);
        }

        /// <summary>
        /// Преобразование к целочисленному типу
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <param name="defaultValue">
        /// Значение по умолчанию
        /// </param>
        /// <returns>
        /// Возвращает <see cref="int"/>, в случае ошибки приведения возвращается значение по умолчанию
        /// </returns>
        public static int ToInt(this object obj, int defaultValue)
        {
            try
            {
                if (obj is bool)
                    return (bool)obj ? 1 : 0;
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Преобразование к целочисленному типу, допускающему null-значение
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="int"/>, в случае ошибки приведения возвращается null
        /// </returns>
        public static int? ToNullInt(this object obj)
        {
            try
            {
                if (obj is bool)
                    return (bool)obj ? 1 : 0;

                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return null;
            }
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
            try
            {
                return Convert.ToDecimal(from);
            }
            catch (Exception)
            {
                return def;
            }
        }

        /// <summary>
        /// Преобразование к значению с плавающей точкой
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="double"/>, в случае ошибки приведения возвращается 0
        /// </returns>
        public static double ToDouble(this object obj)
        {
            return obj.ToDouble(0);
        }

        /// <summary>
        /// Преобразование к значению с плавающей точкой
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <param name="defaultValue">
        /// Значение по умолчанию
        /// </param>
        /// <returns>
        /// Возвращает <see cref="double"/>, в случае ошибки приведения возвращается значение по умолчанию
        /// </returns>
        public static double ToDouble(this object obj, double defaultValue)
        {
            if (obj == null)
                return defaultValue;

            double result;
            try
            {
                result = Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                var str = obj.ToString().Replace('.', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]).Replace(',', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);
                if (!double.TryParse(str, out result))
                    result = defaultValue;
            }
            
            return result;
        }

        /// <summary>
        /// Преобразование к булевому значению
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <returns>
        /// Возвращает <see cref="bool"/>, в случае ошибки приведения возвращается false
        /// </returns>
        public static bool ToBoolean(this object obj)
        {
            return obj.ToBoolean(false);
        }

        /// <summary>
        /// Преобразование к булевому значению
        /// </summary>
        /// <param name="obj">
        /// Преобразуемый объект
        /// </param>
        /// <param name="defaultValue">
        /// Значение по умолчанию
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool ToBoolean(this object obj, bool defaultValue)
        {
            try
            {
                if (obj is bool)
                    return (bool)obj;
                if (obj is string)
                {
                    if (string.IsNullOrEmpty(obj as string))
                        return defaultValue;
                    if (obj as string == "1")
                        return true;
                    if (obj as string == "0")
                        return false;
                }

                if (obj is int)
                    return (int)obj == 1;

                return bool.Parse(obj.ToString());
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Выполняет попытку получения знаения. В случее ошибки NullRefrence в методе получения возвращает значение по умолчанию. 
        /// </summary>
        /// <typeparam name="TObject">Тип источника</typeparam>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="obj">Источник данных</param>
        /// <param name="accessor">Метод получения данных</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Результат работы <paramref name="accessor"/> или <paramref name="defaultValue"/></returns>
        public static TResult TryNoNull<TObject, TResult>(this TObject obj, Func<TObject, TResult> accessor, TResult defaultValue = default(TResult))
        {
            TResult res;
            try
            {
                res = accessor.Invoke(obj);
            }
            catch (NullReferenceException)
            {
                res = default(TResult);
            }

            return res;
        }
    }
}