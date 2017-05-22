using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public sealed class DivisionRepository : Repository<Division>
    {
        public DivisionRepository(DataManager dataManager) :   
            base(new MsSqlDataAccess<Division>(dataManager.GetContext), dataManager)
        {}
    }
}