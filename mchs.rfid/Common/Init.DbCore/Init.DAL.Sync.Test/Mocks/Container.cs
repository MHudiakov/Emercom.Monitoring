namespace Init.DAL.Sync.Test.Mocks
{
    using System;

    class Container
    {
        private static MockDataManager _dataManager;

        public static MockDataManager DataManager
        {
            get
            {
                if (_dataManager == null)
                    throw new Exception("Не зарегистрирован DataManager!");
                return _dataManager;
            }
        }

        public static void RegisterDatamanager(MockDataManager dataManager)
        {
            if (dataManager == null)
                throw new ArgumentNullException("dataManager");

            _dataManager = dataManager;
        }
    }
}
