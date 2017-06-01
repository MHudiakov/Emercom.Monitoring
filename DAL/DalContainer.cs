namespace Server.Dal
{
    using System;

    /// <summary>
    /// Контейнер слоя доступа к данным
    /// </summary>
    public class DalContainer
    {
        /// <summary>
        /// Ссылка на активный менеджер данных
        /// </summary>
        private static volatile DataManager sDataManager;

        /// <summary>
        /// Singletone instance объекта доступа к данным
        /// </summary>
        /// <exception cref="Exception">Если объект не зарегистрирован возбуждается исключение</exception>
        public static DataManager GetDataManager
        {
            get
            {
                if (sDataManager == null)
                {
                    throw new Exception("AISDataManager не зарегистрирован");
                }

                return sDataManager;
            }
        }

        /// <summary>
        /// Зарегистрировать объект доступа к данным в контейнере
        /// </summary>
        /// <remarks>Обеспечивает сущестовование единственного объекта доступа к данным</remarks>
        /// <param name="dataManager">Менеджер данных</param>
        /// <exception cref="Exception">Если объект уже зарегистрирован, возбуждается исключение</exception>
        public static void RegisterDataManger(DataManager dataManager)
        {
            sDataManager = dataManager;
        }
    }
}