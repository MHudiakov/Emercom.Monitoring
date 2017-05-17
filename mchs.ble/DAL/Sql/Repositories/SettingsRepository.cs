using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    public sealed class SettingsRepository : Repository<Settings>
    {
        public SettingsRepository(DataManager dataManager) :   
            base(new MsSqlDataAccess<Settings>(dataManager.GetContext), dataManager)
        { }
    }
}