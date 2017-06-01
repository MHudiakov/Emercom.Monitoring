﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контроллер главной страницы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using DAL.WCF;
using Web.Models.Unit;

namespace Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        /// <summary>
        /// Получение главной страницы
        /// </summary>
        /// <returns>Главная страницы</returns>
        public ActionResult Index()
        {
            var userLogin = User.Identity.Name;
            var user = DalContainer.WcfDataManager.UserList.Single(u => u.Login.Equals(userLogin));
            List<UnitModel> unitList =
                DalContainer.WcfDataManager.ServiceOperationClient.GetUnitListForUser(user.Id).
                    Select(unit
                        => new UnitModel(unit)).ToList();
            return View(unitList);
        }

        /// <summary>
        /// Описание программы
        /// </summary>
        /// <returns>Тест описания из статьи</returns>
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "mchs.ble";
            return View();
        }
    }
}
