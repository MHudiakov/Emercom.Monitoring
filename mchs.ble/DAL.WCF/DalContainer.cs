// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DalContainer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контейнер объектов
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.WCF
{
    using System;

    /// <summary>
    /// Контейнер объектов
    /// </summary>
    public class DalContainer
    {
        /// <summary>
        /// Менеджер данных WCF
        /// </summary>
        private static WcfDataManager _wcfDataManager;

        /// <summary>
        /// Менеджер данных WCF
        /// </summary>
        public static WcfDataManager WcfDataManager
        {
            get
            {
                if (_wcfDataManager == null)
                    throw new Exception("Не зарегистрирован WcfDataManager!");
                return _wcfDataManager;
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

            _wcfDataManager = wcfDataManager;
        }
    }
}