// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlexibleRoleProvider.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Провайдер ролей
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Linq;
    using System.Web.Security;

    using Init.Tools;
    using Init.Web.Resources;

    /// <summary>
    /// Провайдер ролей
    /// </summary>
    public class FlexibleRoleProvider : RoleProvider
    {
        /// <summary>
        /// Событие авторизаци пользователя. 
        /// Если в нем будет сгенерировано исключение, авторизация не произведется
        /// </summary>
        public event Action<MembershipUser> OnAuthorizeUser;

        /// <summary>
        /// Gets or sets the name of the application to store and retrieve role information for.
        /// </summary>
        /// <returns>
        /// The name of the application to store and retrieve role information for.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Адаптер провайдера ролей
        /// </summary>
        private RoleProviderAdapter _adapter;

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param><param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param><exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception><exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception><exception cref="T:System.InvalidOperationException">An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            string defaultAppName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            this.ApplicationName = config.GetString("applicationName", defaultAppName);
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="username">The user to return a list of roles for.</param>
        public override string[] GetRolesForUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            var membershipUser = Membership.Provider.GetUser(username, false);
            if (membershipUser == null)
                throw new ArgumentException(Messages.msgUserNotFound, "username");

            var roles = _adapter.GetRolesForUser(membershipUser);

            if (this.OnAuthorizeUser != null)
                this.OnAuthorizeUser(membershipUser);

            return roles.ToArray();
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// True if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="username">
        /// The user name to search for.
        /// </param>
        /// <param name="roleName">
        /// The role to search in.
        /// </param>
        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");

            var user = Membership.Provider.GetUser(username, false);
            if (user == null)
                throw new ArgumentException(Messages.msgUserNotFound, "username");
            return _adapter.GetRolesForUser(user).Contains(roleName);
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <returns>
        /// True if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="roleName">
        /// The name of the role to search for in the data source.
        /// </param>
        public override bool RoleExists(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");
            return this.GetAllRoles().Contains(roleName);
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            return _adapter.GetAllRoles().ToArray();
        }
        
        /// <summary>
        /// Gets an array of user names in a role where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the users where the user name matches <paramref name="usernameToMatch"/> and the user is a member of the specified role.
        /// </returns>
        /// <param name="roleName">The role to search in.</param><param name="usernameToMatch">The user name to search for.</param>
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException("roleName");
            if (string.IsNullOrWhiteSpace(usernameToMatch))
                throw new ArgumentNullException("usernameToMatch");
            return this.GetUsersInRole(roleName).Where(u => u.Contains(usernameToMatch)).ToArray();
        }

        /// <summary>
        /// Возвращат пользователей по роли
        /// </summary>
        /// <param name="roleName">Роль</param>
        /// <returns>Пользователи имеющие указанную роль</returns>
        public override string[] GetUsersInRole(string roleName)
        {
            return _adapter.GetUsersInRole(roleName);
        }

        /// <summary>
        /// Создает роль
        /// </summary>
        /// <param name="roleName">Название роли</param>
        public override void CreateRole(string roleName)
        {
            _adapter.CreateRole(roleName);
        }

        /// <summary>
        /// Удаляет роль
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="throwOnPopulatedRole">Флаг: бросать исклчение, если нельзя удалить роль вместо возврата false</param>
        /// <returns>Флаг: true, если роль успешно удалена</returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            return _adapter.DeleteRole(roleName, throwOnPopulatedRole);
        }

        /// <summary>
        /// Добавляет пользователям роли
        /// </summary>
        /// <param name="usernames">Пользователи</param>
        /// <param name="roleNames">Роли</param>
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            _adapter.AddUsersToRoles(usernames, roleNames);
        }

        /// <summary>
        /// Удаляет роли у пользователей
        /// </summary>
        /// <param name="usernames">Пользователи</param>
        /// <param name="roleNames">Роли</param>
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            _adapter.RemoveUsersFromRoles(usernames, roleNames);
        }
    }
}