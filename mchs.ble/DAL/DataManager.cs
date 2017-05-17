using System;
using DAL;
using Init.DAL.Sync.GeneralDataPoint;
using Init.DbCore;
using Init.DbCore.DataAccess;
using Init.Tools;
using Server.Dal.Sql.DataObjects;
using Server.Dal.Sql.Repositories;

namespace Server.Dal
{
    /// <summary>
    /// Дата-менеджер
    /// </summary>
    public sealed class DataManager : Init.DbCore.DataManager
    {
        /// <summary>
        /// Логер
        /// </summary>
        public Loger Loger { get; } = new Loger();

        /// <summary>
        /// Cтратегия универсальной точки обмена данными
        /// </summary>
        public StandartDataPointStrategy DataPointStrategy { get; } = new StandartDataPointStrategy();

        /// <summary>
        /// Конструктор дата-менеджера
        /// </summary>
        /// <param name="context">Контекст</param>
        public DataManager(DataContext context)
            : base(context)
        {
            /////////////////////////////////////
            // Создаем и регистрируем репозитории
            /////////////////////////////////////

            // Репозиторий групп оборудования
            RegisterRepository(new EquipmentGroupRepository(this));
            _equipmentGroupRepository =
                new Lazy<EquipmentGroupRepository>(() => GetRepository<EquipmentGroup>() as EquipmentGroupRepository,
                    true);

            // Репозиторий подразделений
            RegisterRepository(new DivisionRepository(this));
            _divisionRepository =
                new Lazy<DivisionRepository>(() => GetRepository<Division>() as DivisionRepository,
                    true);

            // Репозиторий пользователей
            RegisterRepository(new UserRepository(this));
            _userRepository =
                new Lazy<UserRepository>(() => GetRepository<User>() as UserRepository,
                    true);

        }

        /// <summary>
        /// Регистрирует экземпляр репозитория
        /// </summary>
        /// <typeparam name="T">Тип репозитория</typeparam>
        /// <param name="dataAccess">Экземпляр репозитория</param>
        public new void RegisterRepository<T>(DataAccess<T> dataAccess)
            where T : DbObject
        {
            base.RegisterRepository(dataAccess);
            DataPointStrategy.RegisterDataAccess(dataAccess);
        }

        /// <summary>
        /// Контекст доступа к БД
        /// </summary>
        /// <returns>
        /// The <see cref="DataContext"/>.
        /// </returns>
        public new DataContext GetContext()
        {
            return (DataContext) base.GetContext();
        }
        
        #region EquipmentGroupRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий групп
        /// </summary>
        private readonly Lazy<EquipmentGroupRepository> _equipmentGroupRepository;

        /// <summary>
        /// Репозиторий групп оборудования
        /// </summary>
        public EquipmentGroupRepository EquipmentGroupRepository => _equipmentGroupRepository.Value;

        #endregion

        #region DivisionRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий подразделений
        /// </summary>
        private readonly Lazy<DivisionRepository> _divisionRepository;

        /// <summary>
        /// Репозиторий подразделений
        /// </summary>
        public DivisionRepository DivisionRepository => _divisionRepository.Value;

        #endregion

        #region UserRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий пользователей
        /// </summary>
        private readonly Lazy<UserRepository> _userRepository;

        /// <summary>
        /// Репозиторий пользователей
        /// </summary>
        public UserRepository UserRepository => _userRepository.Value;

        #endregion
    }
}