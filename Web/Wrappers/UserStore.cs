using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ble.Common.Enums;
using DAL.WCF;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;

namespace Web.Wrappers
{
    public class UserStore : IUserRoleStore<User, int>
    {
        public void Dispose()
        {
        }

        public Task CreateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.AddUser(user));
        }

        public Task UpdateAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.EditUser(user));
        }

        public Task DeleteAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.DeleteUser(user));
        }

        public Task<User> FindByIdAsync(int userId)
        {
            return Task.Run(() => DalContainer.WcfDataManager.UserList.Single(user => user.Id == userId));
        }

        public Task<User> FindByNameAsync(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));
            return Task.Run(() => DalContainer.WcfDataManager.UserList.SingleOrDefault(user => user.Login.Equals(userName)));
        }

        public Task AddToRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }
        
        public Task<IList<string>> GetRolesAsync(User user)
        {
            return Task.Run(() => (IList<string>)new List<string> {user.Role.ToString()});
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.Run(() =>
            {
                UserRole result;
                if (Enum.TryParse(roleName, out result))
                    return user.Role == result;

                return false;
            });
        }
    }
}