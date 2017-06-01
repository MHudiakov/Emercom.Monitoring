using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public class EquipmentRepository : Repository<Equipment>
    {
        public EquipmentRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Equipment>(dataManager.GetContext), dataManager)
        {}
    }
}