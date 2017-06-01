using System.Linq;
using System.Web.Mvc;
using DAL.WCF;
using Web.Models.User;

namespace Web.Controllers
{
    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Authorize (Roles = UserRolesConsts.Administrator)]
    public class UserController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var filter = new FilterUserModel();
            return View(filter);
        }

        public PartialViewResult List(FilterUserModel filter)
        {
            var userList = filter.DivisionId == null ? 
                DalContainer.WcfDataManager.UserList : 
                DalContainer.WcfDataManager.UserList.Where(user => user.DivisionId == filter.DivisionId);
            
            var userModelList = userList.Select(user => new UserModel(user)).OrderBy(user => user.IsAdmin).ThenBy(user => user.Name).ToList();
            return PartialView(userModelList);
        }
    }
}