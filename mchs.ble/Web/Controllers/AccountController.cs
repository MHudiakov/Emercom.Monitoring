using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ble.Common;
using DAL.WCF;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;
using Web.Models;
using DAL.WCF.Wrappers;

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

        /// <summary>
        /// Post метод авторизации пользователя
        /// </summary>
        /// <param name="model">
        /// Модель авторизации
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        //[HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View();

        //    string passwordHash = CryptographyHelper.GetSha256Hash(model.Password);
        //    var user = DalContainer.WcfDataManager.UserList.SingleOrDefault(
        //        item => item.Login.Equals(model.UserLogin) && item.PasswordHash.Equals(passwordHash));

        //    if (user != null)
        //    {
        //        var authTicket = new FormsAuthenticationTicket(1, model.UserLogin, DateTime.Now, DateTime.Now.AddDays(1), false, user.Role.ToString(), "/");
        //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
        //        Response.Cookies.Add(cookie);
        //        Response.Cookies.Add(cookie);
        //        return this.Redirect(Url.Action("Index", "Home"));
        //    }

        //    this.ModelState.AddModelError(string.Empty, @"Не верное имя пользователя, или пароль");
        //    return View();
        //}


        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.UserLogin, model.Password);
                if (user != null)
                {
                    await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    return this.RedirectToAction("Index", "Home");
                }

                this.ModelState.AddModelError(string.Empty, @"Некорректно введен пароль");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

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
            FormsAuthentication.SignOut();
            return this.View("Login");
        }
    }
}