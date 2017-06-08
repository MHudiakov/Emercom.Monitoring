using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Web.Controllers
{
    using DAL.WCF;
    using DAL.WCF.ServiceReference;
    using Models.EquipmetGroup;

    [Authorize(Roles = UserRolesConsts.Administrator)]
    public class EquipmentGroupController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var filter = new FilterEquipmentGroupModel();
            return View(filter);
        }

        public PartialViewResult EquipmentGroupList(FilterEquipmentGroupModel filter)
        {
            var groupList =
                DalContainer.WcfDataManager.ServiceOperationClient.GetGroupList()
                    .Where(item => !item.ParentId.HasValue)
                    .ToList();
            var sortList = new List<EquipmentGroup>();
            foreach (var group in groupList)
            {
                sortList.Add(group);
                sortList.AddRange(group.ChildrenList);
            }

            // фильтруем группы
            if (!filter.SearchPattern.IsEmpty())
            {
                sortList = sortList.Where(group => group.Name.ToLower().Contains(filter.SearchPattern.ToLower()))
                    .ToList();
            }

            var groupModelList = sortList.Select(equipmentGroup => new EquipmentGroupModel(equipmentGroup)).ToList();
            return PartialView(groupModelList);
        }

        public PartialViewResult KEquipmentList(int id)
        {
            var equipmentList =
                DalContainer.WcfDataManager.ServiceOperationClient.GetKEquipmentList()
                    .Where(item => item.EquipmentGroupId == id).ToList();

            var equipmentModelList = equipmentList.Select(kEquipment => new KEquipmentModel(kEquipment)).ToList();
            return PartialView(equipmentModelList);
        }
    }
}
