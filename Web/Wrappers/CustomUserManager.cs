using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Web.Wrappers
{
    public class CustomUserManager : UserManager<AuthUser, int>
    {
        public CustomUserManager(IUserStore<AuthUser, int> store) : base(store)
        {
            this.PasswordHasher = new CustomPasswordHasher();
        }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            var manager = new CustomUserManager(new UserStore());

            manager.UserValidator = new UserValidator<AuthUser, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            var dataProtectionProvider = options.DataProtectionProvider;

            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<AuthUser, int>(dataProtectionProvider.Create("PasswordReset"));
            }

            return manager;
        }

        public override Task<AuthUser> FindAsync(string userName, string password)
        {
            Task<AuthUser> taskInvoke = Task<AuthUser>.Factory.StartNew(() =>
            {
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
                return result == PasswordVerificationResult.SuccessRehashNeeded 
                    ? Store.FindByNameAsync(userName).Result 
                    : null;
            });
            return taskInvoke;
        }
    }
}
