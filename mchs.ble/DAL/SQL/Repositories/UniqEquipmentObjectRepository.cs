// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UniqEquipmentObjectRepository.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Репозиторий уникального оборудования для машины
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using DAL;

    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Server.Dal.SQL.DataObjects;

    /// <summary>
    /// Репозиторий уникального оборудования для машины
    /// </summary>
    public class UniqEquipmentObjectRepository : Repository<UniqEquipmentObject>
    {
        /// <summary>
        /// Конструктор репозитория уникального оборудования для машины
        /// </summary>
        /// <param name="dataManager">
        /// Дата-менеджер
        /// </param>
        public UniqEquipmentObjectRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<UniqEquipmentObject>(dataManager.GetContext), dataManager)
        {
        }

        /// <summary>
        /// Получение списка уникального оборудования
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public List<UniqEquipmentObject> GetUniqEquipmentObjectListByUnitId(int unitId)
        {
            var list = new List<UniqEquipmentObject>();
            var obj = DalContainer.DataManager.UnitRepository.Get(unitId);
            var store = DalContainer.DataManager.UnitRepository.GetAll().FirstOrDefault(u => u.IsStore);
            var helpList = obj.IsStore
                               ? DalContainer.DataManager.UniqEquipmentObjectRepository.GetItemsWhere(
                                   e => e.UnitId,
                                   unitId).ToList()
                               : DalContainer.DataManager.UniqEquipmentObjectRepository.GetAll()
                                     .Where(eq => eq.UnitId != store.Id)
                                     .ToList();

            foreach (var item in helpList)
            {
                var uniqEquipment = DalContainer.DataManager.UniqEquipmentObjectRepository.Get(item.Id);
                var equipment = DalContainer.DataManager.EquipmentRepository.Get(uniqEquipment.EquipmentId);
                if (equipment == null)
                    continue;

                if (obj.IsStore || uniqEquipment.UnitId == obj.Id)
                    list.Add(uniqEquipment);
                else
                {
                    if (equipment.LastUnitId == obj.Id && equipment.IsArrived)
                        list.Add(uniqEquipment);
                }
            }
            return list;
        }
    }
}
