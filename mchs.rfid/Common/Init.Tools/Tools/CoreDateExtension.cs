// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreDateExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Методы для рабооты с днями
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;

    /// <summary>
    /// Методы для рабооты с днями
    /// </summary>
    public static class CoreDateExtension
    {
        /// <summary>
        /// Получение номера дня месяца.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <returns>
        /// Возвращается день месяца указанной даты
        /// </returns>
        public static int GetDayOfMonth(DateTime date)
        {
            return date.Day;
        }

        /// <summary>
        /// Получаем день недели (1 - понедельник, 7 - воскресенье)
        /// </summary>
        /// <param name="dateTime">
        /// Дата 
        /// </param>
        /// <returns>
        /// Число, представляющее текущий день недели от 1 - понедельник, до 7 - воскресенье
        /// </returns>
        public static int GetDayOfWeek(this DateTime dateTime)
        {
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Конвертирует дату в unix-формат
        /// </summary>
        /// <param name="dt">Дата</param>
        /// <returns>Дата в unix-формате</returns>
        public static double ToUnixTime(this DateTime dt)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return (dt - origin).TotalMilliseconds;
        }
    }
}