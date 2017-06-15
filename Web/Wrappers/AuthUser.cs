using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;

namespace Web.Wrappers
{
    public class AuthUser : User, IUser<int>
    {
        public string UserName
        {
            get { return Login; }
            set { Login = value; }
        }
    }
}