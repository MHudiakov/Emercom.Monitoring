using System.Linq;
using Ble.Common;
using Microsoft.AspNet.Identity;

namespace DAL.WCF.Wrappers
{
    public class CustomPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return CryptographyHelper.GetSha256Hash(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string login, string providedPassword)
        {

            var user = DalContainer.WcfDataManager.UserList.SingleOrDefault(u => u.Login.Equals(login));
            if (user == null)
                return PasswordVerificationResult.Failed;

            return CryptographyHelper.GetSha256Hash(providedPassword).Equals(user.PasswordHash) 
                ? PasswordVerificationResult.SuccessRehashNeeded 
                : PasswordVerificationResult.Failed;
        }
    }
}
