namespace Server.Dal.SQL.Repositories
{
    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Sql.DataObjects;
    
    public class UnitRepository : Repository<Unit>
    {
        public UnitRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Unit>(dataManager.GetContext), dataManager)
        {
        }
    }
}
