// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StorageRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Репозиторий наличия оборудования на складе
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL.SQL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;

    using Server.Dal.Sql.DataObjects;
    using Server.Dal.SQL.DataObjects;

    // todo удаление перегрузить ?
    /// <summary>
    /// Репозиторий наличия оборудования на складе
    /// </summary>
    public class EquipmentRepository : Repository<Equipment>
    {
        /// <summary>
        /// Конструктор репозитория наличия оборудования на складе
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public EquipmentRepository(DalDataManager dataManager)
            : base(new MsSqlDataAccess<Equipment>(dataManager.GetContext), dataManager)
        {
        }

        protected override void AddOverride(Equipment item)
        {
            base.AddOverride(item);

            var tag = DalContainer.DataManager.TagRepository.GetAll().FirstOrDefault(t => t.Id == item.TagId);
            tag.EquipmentId = item.Id;
            DalContainer.DataManager.TagRepository.Edit(tag);

            // склад выбрать по-другому!!
            var stores = DalContainer.DataManager.kObjectRepository.GetAll().Where(s => s.IsStore).ToList();
            var units = DalContainer.DataManager.UnitRepository.GetAll();
            var store = new Unit();

            foreach (var typeStore in stores)
            {
                if (units.FirstOrDefault(u => u.kObjectId == typeStore.Id) != null)
                    store = units.FirstOrDefault(u => u.kObjectId == typeStore.Id);
            }

            var kequipment = DalContainer.DataManager.kEquipmentRepository.GetAll().FirstOrDefault(eq => eq.Id == item.KEquipmentId);

            if (kequipment.IsUniq)
            {
                var uniqEquipment = new UniqEquipmentObject();
                uniqEquipment.EquipmentId = item.Id;
                uniqEquipment.UnitId = store.Id;
                DalContainer.DataManager.UniqEquipmentObjectRepository.Add(uniqEquipment);
            }
            else
            {
                var nonUniqEquipment = DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll()
                        .FirstOrDefault(eq => eq.kEquipmentId == kequipment.Id && eq.UnitId == store.Id);

                if (nonUniqEquipment != null)
                {
                    nonUniqEquipment.Count++;
                    DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(nonUniqEquipment);
                }
                else
                {
                    nonUniqEquipment = new NonUniqEquipmentObject();
                    nonUniqEquipment.kEquipmentId = kequipment.Id;
                    nonUniqEquipment.UnitId = store.Id;
                    nonUniqEquipment.Count = 1;
                    DalContainer.DataManager.NonUniqEquipmentObjectRepository.Add(nonUniqEquipment);
                }
            }
        }

        protected override void EditOverride(Equipment item)
        {
            var oldItem = DalContainer.DataManager.EquipmentRepository.GetAll().FirstOrDefault(eq => eq.Id == item.Id);
            base.EditOverride(item);

            var kEquipmentList = DalContainer.DataManager.kEquipmentRepository.GetAll().ToList();
            var oldkEquipment = kEquipmentList.FirstOrDefault(eq => eq.Id == oldItem.KEquipmentId);
            var newkEquipment = kEquipmentList.FirstOrDefault(eq => eq.Id == item.KEquipmentId);

            if (oldkEquipment != newkEquipment)
            {
                if (!newkEquipment.IsUniq && !oldkEquipment.IsUniq)
                {
                    var oldNonUniqEquipment = DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll()
                                                     .FirstOrDefault(eq => eq.kEquipmentId == oldkEquipment.Id);
                    oldNonUniqEquipment.Count--;
                    DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(oldNonUniqEquipment);

                    var newNonUniqEquipment = DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll()
                                                     .FirstOrDefault(eq => eq.kEquipmentId == newkEquipment.Id);

                    if (newNonUniqEquipment != null)
                    {
                        newNonUniqEquipment.Count++;
                        DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(newNonUniqEquipment);
                    }
                    else
                    {
                        newNonUniqEquipment = new NonUniqEquipmentObject();
                        newNonUniqEquipment.kEquipmentId = oldkEquipment.Id;
                        newNonUniqEquipment.UnitId = oldNonUniqEquipment.UnitId;
                        newNonUniqEquipment.Count = 1;
                        DalContainer.DataManager.NonUniqEquipmentObjectRepository.Add(newNonUniqEquipment);
                    }

                }

                if (!newkEquipment.IsUniq && oldkEquipment.IsUniq)
                {
                    var uniq = DalContainer.DataManager.UniqEquipmentObjectRepository.GetAll()
                                             .FirstOrDefault(eq => eq.EquipmentId == item.Id);
                    DalContainer.DataManager.UniqEquipmentObjectRepository.Delete(uniq.Id);

                    var newNonUniqEquipment = DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll()
                                                     .FirstOrDefault(eq => eq.kEquipmentId == newkEquipment.Id);

                    if (newNonUniqEquipment != null)
                    {
                        newNonUniqEquipment.Count++;
                        DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(newNonUniqEquipment);
                    }
                    else
                    {
                        newNonUniqEquipment = new NonUniqEquipmentObject();
                        newNonUniqEquipment.kEquipmentId = oldkEquipment.Id;
                        newNonUniqEquipment.UnitId = uniq.UnitId;
                        newNonUniqEquipment.Count = 1;
                        DalContainer.DataManager.NonUniqEquipmentObjectRepository.Add(newNonUniqEquipment);
                    }

                }

                if (newkEquipment.IsUniq && !oldkEquipment.IsUniq)
                {
                    var oldNonUniqEquipment = DalContainer.DataManager.NonUniqEquipmentObjectRepository.GetAll()
                                                     .FirstOrDefault(eq => eq.kEquipmentId == oldkEquipment.Id);
                    oldNonUniqEquipment.Count--;
                    DalContainer.DataManager.NonUniqEquipmentObjectRepository.Edit(oldNonUniqEquipment);

                    var uniqEquipment = new UniqEquipmentObject();
                    uniqEquipment.EquipmentId = item.Id;
                    uniqEquipment.UnitId = oldNonUniqEquipment.Id;
                    DalContainer.DataManager.UniqEquipmentObjectRepository.Add(uniqEquipment);

                }
            }

            var oldTag = DalContainer.DataManager.TagRepository.GetAll().FirstOrDefault(t => t.EquipmentId == item.Id);
            if (oldTag.Id != item.TagId)
            {
                oldTag.EquipmentId = null;
                DalContainer.DataManager.TagRepository.Edit(oldTag);

                var newTag = DalContainer.DataManager.TagRepository.GetAll().FirstOrDefault(t => t.Id == item.TagId);
                newTag.EquipmentId = item.Id;
                DalContainer.DataManager.TagRepository.Edit(newTag);
            }
        }

        /// <summary>
        /// Получение оборудования по поездке
        /// </summary>
        /// <param name="tripId"></param>
        /// <param name="isStart"></param>
        /// <returns></returns>
        public List<Equipment> GetComplectationByTripId(int tripId, bool isStart)
        {
            var tripComplectationList =
                DalContainer.DataManager.TripComplectationRepository.GetItemsWhere(e => e.TripId, tripId)
                    .Where(e => e.IsStart == isStart)
                    .ToList();

            var equipmentList = new List<Equipment>();
            tripComplectationList.ForEach(e => equipmentList.Add(e.Equipment));

            return equipmentList;
        }

        /// <summary>
        /// Получение списка оборудования в объекте
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<Equipment> GetСomplectationListByUnitId(int unitId)
        {
            var allEquipments = DalContainer.DataManager.EquipmentRepository.GetAll().ToList();
            return allEquipments.Where(equipment => equipment.LastMovement.UnitId == unitId && equipment.LastMovement.IsArrived).ToList();
        }
    }
}