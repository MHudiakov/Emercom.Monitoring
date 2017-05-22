using System;
using Init.DAL.Sync.GeneralDataPoint;
using Init.DbCore;
using Init.DbCore.DataAccess;
using Init.Tools;
using Server.Dal.Sql.DataObjects;
using Server.Dal.Sql.Repositories;
using Server.Dal.SQL.Repositories;

namespace Server.Dal
{
    /// <summary>
    /// Дата-менеджер
    /// </summary>
    public sealed class DataManager : Init.DbCore.DataManager
    {
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

            Log.Add("Info", "Start creating repos");

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

            // Репозиторий настроек
            RegisterRepository(new SettingsRepository(this));
            _settingsRepository =
                new Lazy<SettingsRepository>(() => GetRepository<Settings>() as SettingsRepository,
                    true);

            // Репозиторий юнитов
            RegisterRepository(new UnitRepository(this));
            _unitRepository =
                new Lazy<UnitRepository>(() => GetRepository<Unit>() as UnitRepository,
                    true);

            // Репозиторий классификаторов оборудования
            RegisterRepository(new KEquipmentRepository(this));
            _kEquipmentRepository =
                new Lazy<KEquipmentRepository>(() => GetRepository<KEquipment>() as KEquipmentRepository,
                    true);

            // Репозиторий оборудования
            RegisterRepository(new EquipmentRepository(this));
            _equipmentRepository =
                new Lazy<EquipmentRepository>(() => GetRepository<Equipment>() as EquipmentRepository,
                    true);

            // Репозиторий передвижений оборудования
            RegisterRepository(new MovementRepository(this));
            _movementRepository =
                new Lazy<MovementRepository>(() => GetRepository<Movement>() as MovementRepository,
                    true);

            Log.Add("Info", "Finished creating repos");
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

        #region SettingsRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий настроек
        /// </summary>
        private readonly Lazy<SettingsRepository> _settingsRepository;

        /// <summary>
        /// Репозиторий настроек
        /// </summary>
        public SettingsRepository SettingsRepository => _settingsRepository.Value;

        #endregion

        #region UnitRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий юнитов
        /// </summary>
        private readonly Lazy<UnitRepository> _unitRepository;

        /// <summary>
        /// Репозиторий юнитов
        /// </summary>
        public UnitRepository UnitRepository => _unitRepository.Value;

        #endregion

        #region KEquipmentRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий классификаторов оборудования
        /// </summary>
        private readonly Lazy<KEquipmentRepository> _kEquipmentRepository;

        /// <summary>
        /// Репозиторий классификаторов оборудования
        /// </summary>
        public KEquipmentRepository KEquipmentRepository => _kEquipmentRepository.Value;

        #endregion

        #region EquipmentRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий оборудования
        /// </summary>
        private readonly Lazy<EquipmentRepository> _equipmentRepository;

        /// <summary>
        /// Репозиторий оборудования
        /// </summary>
        public EquipmentRepository EquipmentRepository => _equipmentRepository.Value;

        #endregion

        #region MovementRepo

        /// <summary>
        /// Кешированная ссылка на репозиторий передвижений оборудования
        /// </summary>
        private readonly Lazy<MovementRepository> _movementRepository;

        /// <summary>
        /// Репозиторий передвижений оборудования
        /// </summary>
        public MovementRepository MovementRepository => _movementRepository.Value;

        #endregion
    }
}