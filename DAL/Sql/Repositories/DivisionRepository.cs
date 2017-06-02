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
        /// Получить список подразделений в древовидной структуре
        /// </summary>
        /// <returns></returns>
        public List<Division> GetTreeSortedList()
        {
            var sortedList = new List<Division>();
            var parents = GetAll().Where(d => !d.ParentId.HasValue).ToList();
            AddChildren(parents, sortedList);
            return sortedList;
        }

        /// <summary>
        /// Рекурсивная процедура получения детей
        /// </summary>
        /// <param name="parentList">
        /// Коллекция родителей, для которой получаем потомков
        /// </param>
        /// <param name="sortedList"></param>
        private void AddChildren(ICollection<Division> parentList, ICollection<Division> sortedList)
        {
            foreach (var item in parentList)
            {
                sortedList.Add(item);
                AddChildren(item.ChildrenList, sortedList);
            }
        }
    }
}