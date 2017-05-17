using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public sealed class UserRepository : Repository<User>
    {
        public UserRepository(DataManager dataManager) : 
            base(new MsSqlDataAccess<User>(dataManager.GetContext), dataManager)
        {}
    }
}