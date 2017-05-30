using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;
using Web.Models;
using Microsoft.Owin.Security;
using Web.Wrappers;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly CustomUserManager _userManager;

        public AccountController()
            : this(new CustomUserManager(new UserStore()))
        {
        }

        public AccountController(CustomUserManager userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// Возвращает View авторизации пользователя в системе
        /// </summary>
        /// <returns>
        /// View авторизации пользователя в системе
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.UserLogin, model.Password);
                if (user != null)
                {
                    // await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    await SignInAsync(user, true);
                    return this.RedirectToAction("Index", "Home");
                }

                this.ModelState.AddModelError(string.Empty, @"Некорректно введен пароль");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Завершение работы пользователя в системе
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}