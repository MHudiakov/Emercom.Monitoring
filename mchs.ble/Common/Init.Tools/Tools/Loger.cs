// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Loger.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Обертка над системой логирования
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System;

    /// <summary>
    /// Обертка над системой логирования
    /// </summary>
    public class Loger
    {
        // todo: добавить sender в событие логирования

        /// <summary>
        /// Логирование информационных сообщений службы
        /// </summary>
        public event Action<string> OnLogMsg;

        /// <summary>
        /// Логирование ошибок службы
        /// </summary>
        public event Action<Exception> OnLogException;

        // todo: добавить типы логирования Info,Debug,Warning,Error

        /// <summary>
        ///  Логирование сервисных сообщений
        /// </summary>
        /// <param name="msg">Сообщение</param>
        public void LogMsg(string msg)
        {
            if (this.OnLogMsg != null)
                this.OnLogMsg(msg);
        }

        /// <summary>
        /// Логирование ошибок
        /// </summary>
        /// <param name="ex">Ошибка</param>
        public void LogException(Exception ex)
        {
            if (this.OnLogException != null)
                this.OnLogException(ex);
        }
    }
}
