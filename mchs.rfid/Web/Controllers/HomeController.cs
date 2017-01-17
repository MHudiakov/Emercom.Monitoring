// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Контроллер главной страницы
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Web.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Контроллер главной страницы
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Получение главной страницы
        /// </summary>
        /// <returns>Главная страницы</returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// Описание программы
        /// </summary>
        /// <returns>Тест описания из статьи</returns>
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "mchs.rfid";

            return View();
        }
    }
}
