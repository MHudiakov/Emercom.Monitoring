// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreConsole.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Сигнал консоли
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Сигнал консоли
    /// </summary>
    public enum ConsoleSignal
    {
        /// <summary>
        /// Нажато сочетание Ctrl-C
        /// </summary>
        CtrlC = 0,

        /// <summary>
        /// Нажато сочетание Ctrl-Break
        /// </summary>
        CtrlBreak = 1,

        /// <summary>
        /// Консоль закрыта
        /// </summary>
        Close = 2,

        /// <summary>
        /// Пользователь завершил сеанс
        /// </summary>
        LogOff = 5,

        /// <summary>
        /// Завершение работы
        /// </summary>
        Shutdown = 6
    }

    /// <summary>
    /// Метод обработки сигнала консоли
    /// </summary>
    /// <param name="consoleSignal">Сигнал консоли</param>
    public delegate void SignalHandler(ConsoleSignal consoleSignal);

    /// <summary>
    /// Класс утилит консоли
    /// </summary>
    public static class CoreConsole
    {
        /// <summary>
        /// Обработчик сигналов консоли. 
        /// </summary>
        /// <param name="handler">Функция обработки сигнала</param>
        /// <param name="add">Флаг добавления обработчка</param>
        /// <returns>Флаг успешности установки обработчика</returns>
        [DllImport("Kernel32", EntryPoint = "SetConsoleCtrlHandler")]
        public static extern bool SetSignalHandler(SignalHandler handler, bool add);
    }
}
