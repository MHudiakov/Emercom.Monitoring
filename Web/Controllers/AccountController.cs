using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.UserLogin, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, true);

                    // Write to cookies server type (global/local)
                    HttpCookie cookie = new HttpCookie("Server type");
                    cookie.Value = model.IsLocalServer.ToString();
                    Response.SetCookie(cookie);

                    if (model.IsLocalServer)
                    {
                        return this.RedirectToAction("Index", "UnitComplectation", new { unitId = 1 });
                    }
                    else
                    {
                        return this.RedirectTo(returnUrl);
                    }
                }

                this.ModelState.AddModelError(string.Empty, @"Некорректно введен логин или пароль");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private async Task SignInAsync(AuthUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private ActionResult RedirectTo(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}