// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DalContainer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контейнер слоя доступа к данным
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL
{
    using System;

    /// <summary>
    /// Контейнер слоя доступа к данным
    /// </summary>
    public class DalContainer
    {
        /// <summary>
        /// Зарегистрировать объект доступа к данным в контейнере
        /// </summary>
        /// <remarks>Обеспечивает сущестовование единственного объекта доступа к данным</remarks>
        /// <param name="dataManager">Менеджер данных</param>
        /// <exception cref="Exception">Если объект уже зарегистрирован, возбуждается исключение</exception>
        public static void RegisterDataManger(DalDataManager dataManager)
        {
            s_dalDataManager = dataManager;
        }

        /// <summary>
        /// Ссылка на активный менеджер данных
        /// </summary>
        private static volatile DalDataManager s_dalDataManager;

        /// <summary>
        /// Singletone instance объекта доступа к данным
        /// </summary>
        /// <exception cref="Exception">Если объект не зарегистрирован возбуждается исключение</exception>
        public static DalDataManager DataManager
        {
            get
            {
                if (s_dalDataManager == null)
                    throw new Exception("AISDataManager не зарегистрирован");
                return s_dalDataManager;
            }
        }
    }
}