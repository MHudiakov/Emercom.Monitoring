// -----------------------------------------------------------------------
// <copyright file="GeoResult.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Maps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GeoResult
    {
        /// <summary>
        /// Результат
        /// </summary>
        public List<GeoObject> List { get; set; }
        
        /// <summary>
        /// Какой был запрос
        /// </summary>
        public string Request { get; set; }
        
        /// <summary>
        /// Если была ошибка в запросе - тут будет более верное наименование (с тегами <fix></fix>) 
        /// </summary>
        public string Suggest { get; set; }

        /// <summary>
        /// Время начала обработки запроса
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания обработки запроса
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Время выполнения
        /// </summary>
        public TimeSpan Duration {
            get
            {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// Успешный ли запрос
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Информация, возвращаемая при ошибке в запросе (описание ошибки)
        /// </summary>
        public string ErrorInfo { get; set; }

        /// <summary>
        /// Ошибка (экземпляр), которая возникла при выполнении запроса.
        /// </summary>
        public Exception Error { get; set; }
    }
}
