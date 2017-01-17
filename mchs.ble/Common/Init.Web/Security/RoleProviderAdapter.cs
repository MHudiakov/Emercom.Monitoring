// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoleProviderAdapter.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Интерфейс адаптера провайдера ролей к источнику данных
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Security
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Web.Security;

    /// <summary>
    /// Адаптер провайдера ролей к источнику данных
    /// </summary>
    public abstract class RoleProviderAdapter : IDisposable
    {
        /// <summary>
        /// Провайдер ролей
        /// </summary>
        public RoleProvider Provider { get; private set; }

        /// <summary>
        /// Адаптер провайдера ролей к источнику данных
        /// </summary>
        /// <param name="provider">Провайдер ролей</param>
        protected RoleProviderAdapter(RoleProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            this.Provider = provider;
        }

        /// <summary>
        /// Инициализация адаптера
        /// </summary>
        /// <param name="config">Коллекция параметров провайдера ролей</param>
        public virtual void Initialize(NameValueCollection config)
        {
        }

        /// <summary>
        /// Выполняет освобождение ресурсов
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Получает список ролей
        /// </summary>
        /// <returns>Список ролей</returns>
        public abstract List<string> GetAllRoles();

        /// <summary>
        /// Получает список ролей пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Список ролей</returns>
        public abstract List<string> GetRolesForUser(MembershipUser user);

        #region NotSupported by default
        /// <summary>
        /// Возвращает список пользователей по роли и паттерну имени пользователя
        /// </summary>
        /// <param name="roleName">Роль</param>
        /// <param name="usernameToMatch">Паттерн имени пользователя</param>
        /// <returns>Список имен пользователей</returns>
        public virtual string[] FindUsersInRole(string roleName, string usernameToMatch)
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
        public virtual string[] GetUsersInRole(string roleName)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Создает роль
        /// </summary>
        /// <param name="roleName">Название роли</param>
        public virtual void CreateRole(string roleName)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Удаляет роль
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="throwOnPopulatedRole">Флаг: бросать исклчение, если нельзя удалить роль вместо возврата false</param>
        /// <returns>Флаг: true, если роль успешно удалена</returns>
        public virtual bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Добавляет пользователям роли
        /// </summary>
        /// <param name="usernames">Пользователи</param>
        /// <param name="roleNames">Роли</param>
        public virtual void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Удаляет роли у пользователей
        /// </summary>
        /// <param name="usernames">Пользователи</param>
        /// <param name="roleNames">Роли</param>
        public virtual void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }
        #endregion
    }
}