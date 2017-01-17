// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClassifierRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Классификатор. Все данные из базы выгружаются при создании репозитория.
//   обращение в базу только при операциях, изменяющих записи или когда не удается найти запись в кеше.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.Repository
{
    using Init.DbCore.DataAccess;

    /// <summary>
    /// Классификатор. Все данные из базы выгружаются при создании репозитория.
    /// обращение в базу только при операциях, изменяющих записи или когда не удается найти запись в кеше.
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    public class ClassifierRepository<T> : CashedRepository<T>
        where T : DbObject
    {
        /// <summary>
        /// Классификатор. Все данные из базы выгружаются при создании репозитория.
        /// обращение в базу только при операциях, изменяющих записи или когда не удается найти запись в кеше.
        /// </summary>
        /// <param name="dataAccess">Объект доступа к данным</param>
        /// <param name="dataManager">DataManager</param>
        public ClassifierRepository(DataAccess<T> dataAccess, DataManager dataManager)
            : base(dataAccess, dataManager)
        {
            foreach (T item in this.DataAccess.GetAll())
                this.Cache.UpdateOrInsert(item, (s, d) => s.CopyTo(d));
        }

        /// <summary>
        /// Шлюз таблицы
        /// </summary>
        protected sealed override DataAccess<T> DataAccess
        {
            get
            {
                return base.DataAccess;
            }
        }

        /// <summary>
        /// Объект операций с кэшем
        /// </summary>
        protected sealed override Cashe<T> Cache
        {
            get
            {
                return base.Cache;
            }
        }
    }
}