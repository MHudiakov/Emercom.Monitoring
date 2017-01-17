// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DalContainer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контейнер объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace ReaderUtility.Dal
{
    /// <summary>
    /// Контейнер объектов
    /// </summary>
    public class DalContainer
    {
        /// <summary>
        /// Менеджер данных WCF
        /// </summary>
        private static WcfDataManager s_wcfDataManager;

        /// <summary>
        /// Менеджер данных WCF
        /// </summary>
        public static WcfDataManager WcfDataManager
        {
            get
            {
                if (s_wcfDataManager == null)
                    throw new Exception("Не зарегистрирован WcfDataManager!");
                return s_wcfDataManager;
            }
        }

        /// <summary>
        /// Зарегистрировать менеджер данных по работе с WCF
        /// </summary>
        /// <param name="wcfDataManager"> 
        /// Менеджер данных 
        /// </param>
        public static void RegisterWcfDataMager(WcfDataManager wcfDataManager)
        {
            if (wcfDataManager == null)
                throw new ArgumentNullException("wcfDataManager");

            s_wcfDataManager = wcfDataManager;
        }
    }
}
