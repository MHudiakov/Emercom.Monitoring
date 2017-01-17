// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreServicesExtension.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Предоставляет пользователю методы работы с процессами и сервисами WIN 32
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools
{
    using System.Diagnostics;
    using System.Linq;
    using System.Management;

    /// <summary>
    /// Предоставляет пользователю методы работы с процессами и сервисами WIN 32
    /// </summary>
    public static class CoreServicesExtension
    {
        /// <summary>
        /// Хранит логическое значение "Является ли процесс сервисом"
        /// </summary>
        private static bool? s_isCurrentProcessIsService;

        /// <summary>
        /// True если текущий процесс является сервсисом
        /// </summary>
        /// <returns>
        /// Если текущий процесс является сервисом, возвращается true, иначе false
        /// </returns>
        public static bool IsService()
        {
            if (!s_isCurrentProcessIsService.HasValue)
                s_isCurrentProcessIsService = IsService((uint)Process.GetCurrentProcess().Id);
            return s_isCurrentProcessIsService.Value;
        }

        /// <summary>
        /// Возвращает true если текущий процесс является сервисом, иначе false 
        /// Использование: IsService((uint)Process.GetCurrentProcess().Id);
        /// </summary>
        /// <param name="pId">
        /// Идентификатор процесса
        /// </param>
        /// <returns>
        /// True если текущий процесс является сервисом, иначе false
        /// </returns>
        public static bool IsService(uint pId)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Service WHERE ProcessId =" + "\"" + pId + "\""))
                {
                    if (searcher.Get().Count > 0)
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Определяет, содержится ли сервис в текущих процессах
        /// </summary>
        /// <param name="name">
        /// Имя сервиса
        /// </param>
        /// <returns>
        /// Возращает true если сервис содержится в текущих процессах, иначе false
        /// </returns>
        public static bool IsServiceExists(string name)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Service WHERE Name =" + "\"" + name + "\""))
                {
                    if (searcher.Get().Cast<ManagementObject>().Any(service => searcher.Get().Count > 0))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}