using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ble.Common.Enums;
using DAL.WCF.ServiceReference;
using Microsoft.AspNet.Identity;

namespace DAL.WCF.Wrappers
{
    public class UserStore : IUserPasswordStore<User, int>, IUserRoleStore<User, int>, IUserLoginStore<User, int>
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

        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            throw new NotImplementedException();
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
            return Task.Run(() => (IList<string>)Enum.GetNames(typeof(UserRole)).ToList());
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }
    }
}