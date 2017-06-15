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
    public class UserStore : IUserRoleStore<AuthUser, int>
    {
        public void Dispose()
        {
        }

        public Task CreateAsync(AuthUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.AddUser(user));
        }

        public Task UpdateAsync(AuthUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.EditUser(user));
        }

        public Task DeleteAsync(AuthUser user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.Run(() => DalContainer.WcfDataManager.ServiceOperationClient.DeleteUser(user));
        }

        public Task<AuthUser> FindByIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                AuthUser authUser = new AuthUser();
                User user = DalContainer.WcfDataManager.UserList.Single(u => u.Id == userId);

                authUser.Id = user.Id;
                authUser.Name = user.Name;
                authUser.Login = user.Login;
                authUser.RoleId = user.RoleId;
                authUser.PasswordHash = user.PasswordHash;
                return authUser;
            });
        }

        public Task<AuthUser> FindByNameAsync(string userName)
        {
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));
            return Task.Run(() =>
            {
                AuthUser authUser = new AuthUser();
                User user = DalContainer.WcfDataManager.UserList.Single(u => u.Login.Equals(userName));

                authUser.Id = user.Id;
                authUser.Name = user.Name;
                authUser.Login = user.Login;
                authUser.RoleId = user.RoleId;
                authUser.PasswordHash = user.PasswordHash;

                return authUser;
            });
        }

        public Task AddToRoleAsync(AuthUser user, string roleName)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(AuthUser user, string roleName)
        {
            throw new NotImplementedException();
        }
        
        public Task<IList<string>> GetRolesAsync(AuthUser user)
        {
            return Task.Run(() => (IList<string>)new List<string> {user.Role.ToString()});
        }

        public Task<bool> IsInRoleAsync(AuthUser user, string roleName)
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