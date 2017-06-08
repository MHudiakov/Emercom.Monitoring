using System;
using DAL.WCF.ServiceReference;
using Web.Models.UnitComplectation;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;

    using Models;

    public class UnitComplectationController : Controller
    {
        [System.Web.Http.HttpGet]
        public ActionResult Index(int unitId)
        {
            // Загружаем формуляр ПТВ для юнита
            IEnumerable<Equipment> equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetEquipmentListForUnit(unitId);

            // Загружаем текущую комплектацию юнита
            IEnumerable<Equipment> currentComplectation = DalContainer.WcfDataManager.ServiceOperationClient
                .GetCurrentComplectationForUnit(unitId);

            // Определяем для каждой записи формуляра ПТВ, находится ли он на данный момент в юните
            equipmentList.ForEach(equipment => equipment.IsInTheUnit = currentComplectation.Any(item => item.Id == equipment.Id));

            // Группируем оборудование по группам
            IEnumerable <IGrouping<EquipmentGroup, Equipment>> equipmentGroups =
                equipmentList.GroupBy(equipment => equipment.KEquipment.EquipmentGroup).OrderBy(group => group.Key.Name);

            List <UnitComplectationModel> unitComplectationModelList =
                equipmentGroups.Select(equipmentGroup => new UnitComplectationModel(equipmentGroup)).ToList();

            return View(unitComplectationModelList);
        }
    }
}
