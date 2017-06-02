using Init.DbCore.DB.MsSql;
using Init.DbCore.Repository;
using Server.Dal.Sql.DataObjects;

namespace Server.Dal.Sql.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    public sealed class DivisionRepository : Repository<Division>
    {
        public DivisionRepository(DataManager dataManager) :   
            base(new MsSqlDataAccess<Division>(dataManager.GetContext), dataManager)
        {}

        /// <summary>
        /// Сортировать список подразделений
        /// </summary>
        /// <returns></returns>
        public List<Division> GetSortedList()
        {
            var divisionList = GetAll();
            var sortedList = new List<Division>();
            foreach (var division in divisionList.Where(d => !d.ParentId.HasValue))
            {
                sortedList.Add(division);
                GetGhilds(division, sortedList);
            }
            return sortedList;
        }

        /// <summary>
        /// Рекурсивная процедура получения детей
        /// </summary>
        /// <param name="parentDivision"></param>
        /// <param name="sortedList"></param>
        /// <param name="divisionList"></param>
        private void GetGhilds(Division parentDivision, List<Division> sortedList )
        {
            foreach (var division in GetAll().Where(d => d.ParentId == parentDivision.Id))
            {
                sortedList.Add(division);
                GetGhilds(division, sortedList);
            }
        }
    }
}