using System.Threading.Tasks;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Web.Wrappers
{
    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager(IUserStore<User, int> store) : base(store)
        {
            this.PasswordHasher = new CustomPasswordHasher();
        }

        public static CustomUserManager Create(IdentityFactoryOptions<CustomUserManager> options, IOwinContext context)
        {
            var manager = new CustomUserManager(new UserStore());

            manager.UserValidator = new UserValidator<User, int>(manager)
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
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("PasswordReset"));
            }

            return manager;
        }

        public override Task<User> FindAsync(string userName, string password)
        {
            Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
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
