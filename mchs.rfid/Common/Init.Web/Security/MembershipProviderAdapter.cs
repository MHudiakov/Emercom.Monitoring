// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MembershipProviderAdapter.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Адаптер провайдера аутентификации
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Security.Cryptography;
    using System.Web.Security;

    /// <summary>
    /// Адаптер провайдера аутентификации
    /// </summary>
    public abstract class MembershipProviderAdapter : IDisposable
    {
        /// <summary>
        /// Провайдер аутентификации
        /// </summary>
        public MembershipProvider Provider { get; private set; }

        /// <summary>
        /// Адаптер провайдера аутентификации
        /// </summary>
        /// <param name="provider">Провайдер аутентификации</param>
        protected MembershipProviderAdapter(MembershipProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            this.Provider = provider;
        }

        /// <summary>
        /// Инициализация адаптера
        /// </summary>
        /// <param name="config">
        /// Коллекция параметров провайдера аутентификации
        /// </param>
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
        /// Выполняет проверку пароля
        /// </summary>
        /// <param name="user">
        /// Пользователь
        /// </param>
        /// <param name="password">
        /// Пароль (нешифрованный)
        /// </param>
        /// <returns>
        /// True, если пароль правльный
        /// </returns>
        public abstract bool TryCheckUserPassword(MembershipUser user, string password);

        /// <summary>
        /// Возвращает, если возможно, пароль пользователя (Проверка механизмов шифрования и валидности вызова данной оерации выполняется базовым классом)
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Пароль</returns>
        public abstract string GetUserPassword(MembershipUser user);

        /// <summary>
        /// Выолняет обновление даты последей актиности пльзователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public abstract void UpdateUserActivityDate(MembershipUser user);

        /// <summary>
        /// Получить запись пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Пользователь</returns>
        public abstract MembershipUser GetUser(string username);

        /// <summary>
        /// Получить запись пользователя
        /// </summary>
        /// <param name="providerUserKey">Уникальный ключ пользователя</param>
        /// <returns>Пользователь</returns>
        public abstract MembershipUser GetUser(object providerUserKey);

        /// <summary>
        /// Возвращает пользователей постранично
        /// </summary>
        /// <param name="pageIndex">Индекс страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <param name="totalRecords">Возвращает общее количество пользователей</param>
        /// <returns>Коллекцию пользователей текущей страницы</returns>
        public abstract MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        /// Генерирует новый пароль пользователя
        /// </summary>
        /// <returns>Пароль</returns>
        public virtual string GenerateNewPassword()
        {
            var passwordBytes = new byte[16];

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(passwordBytes);
            var base64String = Convert.ToBase64String(passwordBytes);
            return base64String;
        }

        #region NotSupported by default
        /// <summary>
        /// Выполняет проверку ответа пользователя на секретный вопрос
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="passwordAnswer">Секретный вопрос</param>
        /// <returns>True, если ответ верный</returns>
        public virtual bool TryCheckPasswordAnswer(MembershipUser user, string passwordAnswer)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Устанавливает новый вопрос и ответ
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="newPasswordQuestion">Вопрос</param>
        /// <param name="newPasswordAnswer">Ответ</param>
        /// <returns>True, если установка прошла успешно</returns>
        public virtual bool TrySetPasswordQuestionAndAnswer(
            MembershipUser user,
            string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Tries the set password.
        /// </summary>
        /// <param name="user">
        /// Пользователь
        /// </param>
        /// <param name="password">
        /// Пароль (нешифрованный)
        /// </param>
        /// <returns>
        /// True, если удалось установить пароль и ответ
        /// </returns>
        public virtual bool TrySetPassword(MembershipUser user, string password)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Удаляет пользователя и з источника данных
        /// </summary>
        /// <returns>
        /// True если информация успешно удалена
        /// </returns>
        /// <param name="user">
        /// Пользователь
        /// </param>
        /// <param name="deleteAllRelatedData">
        /// True если нужно удалитьвсе связанные с пользователем данные
        /// </param>
        public virtual bool DeleteUser(MembershipUser user, bool deleteAllRelatedData)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Добавляет нового пользователя в БД
        /// </summary>
        /// <returns>
        /// Запись MembershipUser 
        /// </returns>
        /// <param name="username"> The user name for the new user.</param>
        /// <param name="password">The password for the new user. </param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
        public virtual MembershipUser CreateUser(
            string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Выполняет разблокировку пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Результат разблокировки</returns>
        public virtual bool UnlockUser(MembershipUser user)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Выполняет обновление пользователя данными указанными в [user]
        /// </summary>
        /// <param name="user">Информация о пользователе</param>
        public virtual void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Поиск пользователей по адресу почты
        /// </summary>
        /// <param name="emailToMatch">
        /// Адрес почты или его часть
        /// </param>
        /// <param name="pageIndex">
        /// Индекс страницы
        /// </param>
        /// <param name="pageSize">
        /// Размер страницы
        /// </param>
        /// <param name="totalRecords">
        /// Возвращает общее количество пользователей
        /// </param>
        /// <returns>
        /// Коллекцию пользователей текущей страницы
        /// </returns>
        public virtual MembershipUserCollection FindUsersByEmail(
            string emailToMatch,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }

        /// <summary>
        /// Поиск пользователей по адресу почты
        /// </summary>
        /// <param name="usernameToMatch">
        /// Логин или его часть
        /// </param>
        /// <param name="pageIndex">
        /// Индекс страницы
        /// </param>
        /// <param name="pageSize">
        /// Размер страницы
        /// </param>
        /// <param name="totalRecords">
        /// Возвращает общее количество пользователей
        /// </param>
        /// <returns>
        /// Коллекцию пользователей текущей страницы
        /// </returns>
        public virtual MembershipUserCollection FindUsersByName(
            string usernameToMatch,
            int pageIndex,
            int pageSize,
            out int totalRecords)
        {
            throw new NotSupportedException("Данная функция не поодерживается. Переопределите соответствуюущий метод.");
        }
        #endregion
    }
}