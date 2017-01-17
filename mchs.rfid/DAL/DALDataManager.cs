// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DalDataManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Дата-менеджер
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL
{
    using System;

    using DAL.SQL.DataObjects;
    using DAL.SQL.Repositories;

    using Init.DAL.Sync.GeneralDataPoint;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.Tools;
    using Server.Dal.SQL.Repositories;
    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Дата-менеджер
    /// </summary>
    public class DalDataManager : DataManager
    {
        /// <summary>
        /// Логер
        /// </summary>
        public Loger Loger { get; private set; }

        /// <summary>
        /// Конструктор дата-менеджера
        /// </summary>
        /// <param name="context">Контекст</param>
        public DalDataManager(DalDataContext context)
            : base(context)
        {
            Loger = new Loger();

            DataPointStrategy = new StandartDataPointStrategy();

            RegisterRepository(new kEquipmentRepository(this));
            RegisterRepository(new GroupRepository(this));
            RegisterRepository(new MovementRepository(this));
            RegisterRepository(new EquipmentRepository(this));
            RegisterRepository(new kObjectRepository(this));
            RegisterRepository(new UnitRepository(this));
            RegisterRepository(new UniqEquipmentObjectRepository(this));
            RegisterRepository(new NonUniqEquipmentObjectRepository(this));
            RegisterRepository(new TagRepository(this));
            RegisterRepository(new GeoPointsRepository(this));
            RegisterRepository(new StoreRepository(this));
            RegisterRepository(new TripRepository(this));
            RegisterRepository(new TripComplectationRepository(this));

            _kEquipmentRepository = new Lazy<kEquipmentRepository>(() => GetRepository<kEquipment>() as kEquipmentRepository, true);
            _groupRepository = new Lazy<GroupRepository>(() => GetRepository<Group>() as GroupRepository, true);
            _movementRepository = new Lazy<MovementRepository>(() => GetRepository<Movement>() as MovementRepository, true);
            _equipmentRepository = new Lazy<EquipmentRepository>(() => GetRepository<Equipment>() as EquipmentRepository, true);
            _kObjectRepository = new Lazy<kObjectRepository>(() => GetRepository<kObject>() as kObjectRepository, true);
            _unitRepository = new Lazy<UnitRepository>(() => GetRepository<Unit>() as UnitRepository, true);
            _uniqEquipmentObjectRepository = new Lazy<UniqEquipmentObjectRepository>(() => GetRepository<UniqEquipmentObject>() as UniqEquipmentObjectRepository, true);
            _nonUniqEquipmentObjectRepository = new Lazy<NonUniqEquipmentObjectRepository>(() => GetRepository<NonUniqEquipmentObject>() as NonUniqEquipmentObjectRepository, true);
            _tagRepository = new Lazy<TagRepository>(() => GetRepository<Tag>() as TagRepository, true);
            _geoPointsRepository = new Lazy<GeoPointsRepository>(() => GetRepository<GeoPoints>() as GeoPointsRepository, true);
            _storeRepository = new Lazy<StoreRepository>(() => GetRepository<Store>() as StoreRepository, true);
            _tripsRepository = new Lazy<TripRepository>(() => GetRepository<Trip>() as TripRepository, true);
            _tripComplectationRepository = new Lazy<TripComplectationRepository>(() => GetRepository<TripComplectation>() as TripComplectationRepository, true);

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
        /// Cтратегия универсальной точки обмена данными
        /// </summary>
        public StandartDataPointStrategy DataPointStrategy { get; private set; }

        /// <summary>
        /// Контекст доступа к БД
        /// </summary>
        /// <returns>
        /// The <see cref="DalDataContext"/>.
        /// </returns>
        public new DalDataContext GetContext()
        {
            return (DalDataContext)base.GetContext();
        }

        #region kEquipment

        /// <summary>
        /// Кешированная ссылка на репозиторий классификатора оборудования
        /// </summary>
        private readonly Lazy<kEquipmentRepository> _kEquipmentRepository;

        /// <summary>
        /// Репозиторий оборудования
        /// </summary>
        public kEquipmentRepository kEquipmentRepository
        {
            get { return _kEquipmentRepository.Value; }
        }

        #endregion

        #region Store
        /// <summary>
        /// Кешированная ссылка на репозиторий баз
        /// </summary>
        private readonly Lazy<StoreRepository> _storeRepository;

        /// <summary>
        /// Репозиторий баз
        /// </summary>
        public StoreRepository StoreRepository
        {
            get { return _storeRepository.Value; }
        }

        #endregion

        #region Trips
        /// <summary>
        /// Кешированная ссылка на репозиторий поездок
        /// </summary>
        private readonly Lazy<TripRepository> _tripsRepository;

        /// <summary>
        /// Репозиторий поездок
        /// </summary>
        public TripRepository TripRepository
        {
            get { return _tripsRepository.Value; }
        }

        #endregion

        #region TripComplectation
        /// <summary>
        /// Кешированная ссылка на репозиторий комплектации
        /// </summary>
        private readonly Lazy<TripComplectationRepository> _tripComplectationRepository;

        /// <summary>
        /// Репозиторий комплектации
        /// </summary>
        public TripComplectationRepository TripComplectationRepository
        {
            get { return _tripComplectationRepository.Value; }
        }

        #endregion

        #region Group

        /// <summary>
        /// Кешированная ссылка на репозиторий групп
        /// </summary>
        private readonly Lazy<GroupRepository> _groupRepository;

        /// <summary>
        /// Репозиторий групп оборудования
        /// </summary>
        public GroupRepository GroupRepository
        {
            get { return _groupRepository.Value; }
        }

        #endregion

        #region Movement

        /// <summary>
        /// Кешированная ссылка на репозиторий движения
        /// </summary>
        private readonly Lazy<MovementRepository> _movementRepository;

        /// <summary>
        /// Репозиторий перемещений по складу
        /// </summary>
        public MovementRepository MovementRepository
        {
            get { return _movementRepository.Value; }
        }

        #endregion

        #region Equipment

        /// <summary>
        /// Кешированная ссылка на репозиторий оборудования
        /// </summary>
        private readonly Lazy<EquipmentRepository> _equipmentRepository;

        /// <summary>
        /// Репозиторий хранилища
        /// </summary>
        public EquipmentRepository EquipmentRepository
        {
            get { return _equipmentRepository.Value; }
        }

        #endregion

        #region kObject

        /// <summary>
        /// Кешированная ссылка на репозиторий типов объектов
        /// </summary>
        private readonly Lazy<kObjectRepository> _kObjectRepository;

        /// <summary>
        /// Репозиторий типов объектов
        /// </summary>
        public kObjectRepository kObjectRepository
        {
            get { return _kObjectRepository.Value; }
        }

        #endregion

        #region Tag

        /// <summary>
        /// Кешированная ссылка на репозиторий тегов
        /// </summary>
        private readonly Lazy<TagRepository> _tagRepository;

        /// <summary>
        /// Репозиторий тегов
        /// </summary>
        public TagRepository TagRepository
        {
            get { return _tagRepository.Value; }
        }

        #endregion

        #region Unit

        /// <summary>
        /// Кешированная ссылка на репозиторий объектов
        /// </summary>
        private readonly Lazy<UnitRepository> _unitRepository;

        /// <summary>
        /// Репозиторий объектов
        /// </summary>
        public UnitRepository UnitRepository
        {
            get { return _unitRepository.Value; }
        }

        #endregion
        #region UniqEquipmentObject

        /// <summary>
        /// Кешированная ссылка на репозиторий уникального оборудования для машины
        /// </summary>
        private readonly Lazy<UniqEquipmentObjectRepository> _uniqEquipmentObjectRepository;

        /// <summary>
        /// Репозиторий уникального оборудования для машины
        /// </summary>
        public UniqEquipmentObjectRepository UniqEquipmentObjectRepository
        {
            get { return _uniqEquipmentObjectRepository.Value; }
        }

        #endregion

        #region NonUniqEquipmentObject

        /// <summary>
        /// Кешированная ссылка на репозиторий не уникального оборудования для машины
        /// </summary>
        private readonly Lazy<NonUniqEquipmentObjectRepository> _nonUniqEquipmentObjectRepository;

        /// <summary>
        /// Репозиторий не уникального оборудования для машины
        /// </summary>
        public NonUniqEquipmentObjectRepository NonUniqEquipmentObjectRepository
        {
            get { return _nonUniqEquipmentObjectRepository.Value; }
        }

        #endregion

        #region NonUniqEquipmentObject

        /// <summary>
        /// Кешированная ссылка на репозиторий гео меток
        /// </summary>
        private readonly Lazy<GeoPointsRepository> _geoPointsRepository;

        /// <summary>
        /// Репозиторий гео меток
        /// </summary>
        public GeoPointsRepository GeoPointsRepository
        {
            get { return _geoPointsRepository.Value; }
        }

        #endregion
    }
}