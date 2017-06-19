using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ble.Common.Enums;
using Microsoft.AspNet.Identity;

namespace Web.Wrappers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly CustomUserManager _customUserManager = new CustomUserManager(new UserStore());

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int id = httpContext.User.Identity.GetUserId<int>();

            if (id <= 0)
                return false;

            string[] roles = Enum.GetValues(typeof(UserRole))
                .Cast<int>()
                .Select(x => x.ToString())
                .ToArray();

            return roles.Any(role => _customUserManager.IsInRole(id, role));
        }
    }
}