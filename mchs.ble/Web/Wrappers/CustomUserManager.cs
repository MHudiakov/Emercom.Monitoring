using System.Threading.Tasks;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;

namespace Web.Wrappers
{
    public class CustomUserManager : UserManager<User, int>
    {
        public CustomUserManager(IUserStore<User, int> store) : base(store)
        {
            this.PasswordHasher = new CustomPasswordHasher();
        }

        public override Task<User> FindAsync(string userName, string password)
        {
            Task<User> taskInvoke = Task<User>.Factory.StartNew(() =>
            {
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
                if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    return Store.FindByNameAsync(userName).Result;
                }
                return null;
            });
            return taskInvoke;
        }
    }
}
