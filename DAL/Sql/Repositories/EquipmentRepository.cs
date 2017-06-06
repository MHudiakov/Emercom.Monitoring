using System.Collections.Generic;
using System.Linq;
using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public class EquipmentRepository : ClassifierRepository<Equipment>
    {
        public EquipmentRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Equipment>(dataManager.GetContext), dataManager)
        {}

        public List<Equipment> GetEquipmentListForUnit(int unitId)
        {
            return this.GetAll().Where(item => item.UnitId == unitId).ToList();
        }

        public List<Equipment> GetCurrentComplectationForUnit(int unitId)
        {
            var result = from equipment in GetAll()
                let lastMovement = equipment.LastMovement
                where equipment.UnitId == unitId &&
                      lastMovement.UnitId == unitId &&
                      lastMovement.IsArrived
                select equipment;

            //var test = this.GetAll().Where(x => x.GetLastMovement)

            return result.ToList();
        }
    }
}