using System.Collections.Generic;
using DAL.WCF.ServiceReference;

namespace Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using DAL.WCF;
    using Models;

    [Authorize]
    public class MovementController : Controller
    {
        [HttpGet]
        public ActionResult Index(int unitId)
        {
            var filter = new FilterMovementModel(unitId);
            filter.DtEnd=DateTime.Now;
            filter.DtBegin=DateTime.Now.AddDays(-5);
            return View(filter);
        }

        public ActionResult List(FilterMovementModel filter)
        {
            var valid = ModelState.IsValid;
            // Проводим валидацию фильтра
            if (filter.DtBegin == null)
                filter.DtBegin = DateTime.Now.AddDays(-5);

            if (filter.DtEnd == null)
                filter.DtEnd = DateTime.Now;
            
            List<Movement> movementList =
                    DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(
                        filter.DtBegin.Value,
                        filter.DtEnd.Value,
                        filter.UnitId);

            if (filter.EquipmentId != null)
                movementList = movementList.Where(item => item.EquipmentId == filter.EquipmentId).ToList();

            var movementModelList =
                movementList.Select(movement => new MovementModel(movement)).OrderBy(movement => movement.Date)
                .ToList();

            return PartialView(movementModelList);
        }
	}
}