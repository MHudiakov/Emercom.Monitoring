namespace Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using DAL.WCF;
    using Models.Settings;
    using Web.Wrappers;

    [CustomAuthorize(Roles = UserRolesConsts.Administrator)]
    public class SettingsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}