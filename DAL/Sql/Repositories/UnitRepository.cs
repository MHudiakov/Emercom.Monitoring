using System;
using System.Collections.Generic;

namespace Server.Dal.SQL.Repositories
{
    using Init.DbCore.DB.MsSql;
    using Init.DbCore.Repository;
    using Sql.DataObjects;
    
    public class UnitRepository : ClassifierRepository<Unit>
    {
        public UnitRepository(DataManager dataManager)
            : base(new MsSqlDataAccess<Unit>(dataManager.GetContext), dataManager)
        {
        }

        public List<Unit> GetUnitListForUser(int userId)
        {
            User user = DalContainer.GetDataManager.UserRepository.Get(userId);

            if (user == null)
                throw new ArgumentException("Пользователь с данным Id не найден в базе данных");

            // Получаем список подразделений, которые данный пользователь вправе просматривать
            var divisionList = new List<Division>();
            divisionList.Add(user.GetDivision);
            divisionList.AddRange(user.GetDivision.ChildrenList);

            List<Unit> result = new List<Unit>();
            foreach (var division in divisionList)
            {
                result.AddRange(division.GetUnitList);
            }

            return result;
        }
    }
}
