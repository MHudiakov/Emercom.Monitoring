﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ble.Common;
using DAL.WCF;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Возвращает View авторизации пользователя в системе
        /// </summary>
        /// <returns>
        /// View авторизации пользователя в системе
        /// </returns>
        [HttpGet]
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
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View();

            string passwordHash = CryptographyHelper.GetSha256Hash(model.Password);
            var user = DalContainer.WcfDataManager.UserList.SingleOrDefault(
                item => item.Login.Equals(model.UserLogin) && item.PasswordHash.Equals(passwordHash));

            if (user != null)
            {
                var authTicket = new FormsAuthenticationTicket(1, model.UserLogin, DateTime.Now, DateTime.Now.AddDays(1), false, user.Role.ToString(), "/");
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                return this.Redirect(Url.Action("Index", "Home"));
            }

            this.ModelState.AddModelError(string.Empty, @"Не верное имя пользователя, или пароль");
            return View();
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