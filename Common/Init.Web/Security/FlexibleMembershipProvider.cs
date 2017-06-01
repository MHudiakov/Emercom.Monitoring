// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FlexibleMembershipProvider.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Defines the MembershipProviderBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Web.Security
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration.Provider;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Security;

    using Init.Tools;
    using Init.Web.Resources;

    /// <summary>
    /// Базовый класс MembershipProvider
    /// </summary>
    public class FlexibleMembershipProvider : MembershipProvider
    {
        #region Fields
        /// <summary>
        /// Адаптер провайдера аутентификации
        /// </summary>
        private MembershipProviderAdapter _adapter;
        #endregion

        #region Properties
        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// Имя приложения
        /// </returns>
        public override string ApplicationName { get; set; }

        #region EnablePasswordReset
        /// <summary>
        /// Флаг: разрешает сброс пароля
        /// </summary>
        private bool _enablePasswordReset;

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
        /// </returns>
        public override bool EnablePasswordReset
        {
            get
            {
                return this._enablePasswordReset;
            }
        }
        #endregion

        #region EnablePasswordRetrieval
        /// <summary>
        /// Флаг: разрешает запрос пароля
        /// </summary>
        private bool _enablePasswordRetrieval;

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        /// </returns>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                return this._enablePasswordRetrieval;
            }
        }
        #endregion

        #region MaxInvalidPasswordAttempts
        /// <summary>
        /// Количество попыток авторизоваться с неправильным паролем (не используется)
        /// </summary>
        private int _maxInvalidPasswordAttempts;

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </returns>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return this._maxInvalidPasswordAttempts;
            }
        }
        #endregion

        #region MinRequiredNonAlphanumericCharacters
        /// <summary>
        /// Минимальное количество неалфавитноцифровых символов требуемых в пароле
        /// </summary>
        private int _minRequiredNonAlphanumericCharacters;

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        /// </returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return this._minRequiredNonAlphanumericCharacters;
            }
        }
        #endregion

        #region MinRequiredPasswordLength
        /// <summary>
        /// Минимальная длина пароля
        /// </summary>
        private int _minRequiredPasswordLength;

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The minimum length required for a password.
        /// </returns>
        public override int MinRequiredPasswordLength
        {
            get
            {
                return this._minRequiredPasswordLength;
            }
        }
        #endregion

        #region PasswordAttemptWindow
        /// <summary>
        /// Окно попыток контроля попыток авторизации для maxInvalidPasswordAttempts (не используется)
        /// </summary>
        private int _passwordAttemptWindow;

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </returns>
        public override int PasswordAttemptWindow
        {
            get
            {
                return this._passwordAttemptWindow;
            }
        }
        #endregion

        #region PasswordFormat
        /// <summary>
        /// Формат хранения пароля (и прочих данных)
        /// </summary>
        private MembershipPasswordFormat _passwordFormat;

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.
        /// </returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return this._passwordFormat;
            }
        }
        #endregion

        #region PasswordStrengthRegularExpression
        /// <summary>
        /// Регулярное выражение для котроля качества пароля
        /// </summary>
        private string _passwordStrengthRegularExpression;

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// A regular expression used to evaluate a password.
        /// </returns>
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return this._passwordStrengthRegularExpression;
            }
        }
        #endregion

        #region RequiresQuestionAndAnswer
        /// <summary>
        /// Флаг: требует ответа на вопрос
        /// </summary>
        private bool _requiresQuestionAndAnswer;

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <value></value>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return this._requiresQuestionAndAnswer;
            }
        }
        #endregion

        #region RequiresUniqueEmail
        /// <summary>
        /// Флаг: требует уникльности эл.почты
        /// </summary>
        private bool _requiresUniqueEmail;

        /// <summary>
        /// Флаг. указывает, что уникальным полем является EMail. 
        /// Если выставлен в true, то EMail должен использоваться для аутентификации. Иначе 
        /// </summary>
        /// <value></value>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresUniqueEmail
        {
            get
            {
                return this._requiresUniqueEmail;
            }
        }
        #endregion
        #endregion

        #region Init
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The name of the provider is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The name of the provider has a length of zero.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// An attempt is made to call <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/> on a provider after the provider has already been initialized.
        /// </exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);

            string defaultAppName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            this.ApplicationName = config.GetString("applicationName", defaultAppName);

            this._enablePasswordReset = config.GetBool("enablePasswordReset", true);
            this._enablePasswordRetrieval = config.GetBool("enablePasswordRetrieval", false);
            this._maxInvalidPasswordAttempts = config.GetInt("maxInvalidPasswordAttempts", 5);
            this._minRequiredNonAlphanumericCharacters = config.GetInt("minRequiredNonAlphanumericCharacters", 0);
            this._minRequiredPasswordLength = config.GetInt("minRequiredPasswordLength", 4);
            this._passwordAttemptWindow = config.GetInt("passwordAttemptWindow", 10);
            this._passwordFormat = config.GetEnum<MembershipPasswordFormat>("passwordFormat");
            this._passwordStrengthRegularExpression = config.GetString("passwordStrengthRegularExpression", @"[\w| !§$%&/()=\-?\*]*");
            this._requiresQuestionAndAnswer = config.GetBool("requiresQuestionAndAnswer", false);
            this._requiresUniqueEmail = config.GetBool("requiresUniqueEmail", true);
        }
        #endregion

        #region Membership Methods
        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">
        /// The user to update the password for.
        /// </param>
        /// <param name="oldPassword">
        /// The current password for the specified user.
        /// </param>
        /// <param name="newPassword">
        /// The new password for the specified user.
        /// </param>
        /// <returns>
        /// True if the password was updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (oldPassword == null)
                throw new ArgumentNullException("oldPassword");
            if (newPassword == null)
                throw new ArgumentNullException("newPassword");

            // проверяет существование пользователя 
            if (!this.ValidateUser(username, oldPassword))
                return false;

            // ищем запись пользователя
            var user = GetUser(username, true);
            if (user == null)
                throw new ProviderException(Messages.msgUserNotFound);

            this._adapter.UpdateUserActivityDate(user);

            if (this.VerifyPasswordIsValid(newPassword))
            {
                // Генерируем событие валидации пароля
                var args = new ValidatePasswordEventArgs(username, newPassword, false);
                this.OnValidatingPassword(args);
                if (args.Cancel)
                    throw new ProviderException("Смена пароля отменена", args.FailureInformation);

                return this._adapter.TrySetPassword(user, newPassword);
            }

            throw new ProviderException("Новый пароль не соответствует политике безопасности");
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">
        /// The user to change the password question and answer for.
        /// </param>
        /// <param name="password">
        /// The password for the specified user.
        /// </param>
        /// <param name="newPasswordQuestion">
        /// The new password question for the specified user.
        /// </param>
        /// <param name="newPasswordAnswer">
        /// The new password answer for the specified user.
        /// </param>
        /// <returns>
        /// True if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (password == null)
                throw new ArgumentNullException("password");
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(newPasswordQuestion))
                throw new ArgumentNullException("newPasswordQuestion", Messages.msgNeedSecretQuestion);
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(newPasswordAnswer))
                throw new ArgumentNullException("newPasswordAnswer", Messages.msgNeedSecretAnswer);

            // проверяет существование пользователя 
            if (!this.ValidateUser(username, password))
                return false;

            // ищем запись пользователя
            var user = GetUser(username, true);
            if (user == null)
                throw new ProviderException(Messages.msgUserNotFound);

            this._adapter.UpdateUserActivityDate(user);

            return this._adapter.TrySetPasswordQuestionAndAnswer(user, newPasswordQuestion, newPasswordAnswer);
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">
        /// The user to retrieve the password for.
        /// </param>
        /// <param name="passwordAnswer">
        /// The password Answer.
        /// </param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        public override string GetPassword(string username, string passwordAnswer)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (!this.EnablePasswordRetrieval)
                throw new NotSupportedException(Messages.msgPasswordRequestForbidden);
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(passwordAnswer))
                throw new ArgumentNullException("passwordAnswer", Messages.msgNeedSecretAnswer);

            // ищем запись пользователя
            var user = GetUser(username, true);
            if (user == null)
                throw new ProviderException(Messages.msgUserNotFound);

            this._adapter.UpdateUserActivityDate(user);

            if (!this._adapter.TryCheckPasswordAnswer(user, passwordAnswer))
                throw new ProviderException(Messages.msgWrongSecretAnswer);

            if (!this.EnablePasswordRetrieval)
                throw new ProviderException(Messages.msgPasswordRequestForbidden);

            if (this.PasswordFormat == MembershipPasswordFormat.Hashed)
                throw new ProviderException(Messages.msgPasswordRetrievingImposible);

            return this._adapter.GetUserPassword(user);
        }

        /// <summary>
        /// Сбрасывает пароль пользователя на новый, автоматически сгенерированный
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="passwordAnswer">Ответ на серктный вопрос</param>
        /// <returns>Новый пароль для указанного пользователя</returns>
        public override string ResetPassword(string username, string passwordAnswer)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (!this.EnablePasswordReset)
                throw new NotSupportedException(Messages.msgPasswordResetForbidden);
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(passwordAnswer))
                throw new ArgumentNullException("passwordAnswer", Messages.msgNeedSecretAnswer);

            // ищем запись пользователя
            var user = GetUser(username, true);
            if (user == null)
                throw new ProviderException(Messages.msgUserNotFound);

            this._adapter.UpdateUserActivityDate(user);

            if (this.RequiresQuestionAndAnswer && !this._adapter.TryCheckPasswordAnswer(user, passwordAnswer))
                throw new ProviderException(Messages.msgWrongSecretAnswer);

            var newPassword = this._adapter.GenerateNewPassword();

            if (this.VerifyPasswordIsValid(newPassword))
            {
                // Генерируем событие валидации пароля
                var args = new ValidatePasswordEventArgs(username, newPassword, false);
                this.OnValidatingPassword(args);
                if (args.Cancel)
                    throw new ProviderException(Messages.msgPasswodResetRejected, args.FailureInformation);

                if (this._adapter.TrySetPassword(user, newPassword))
                    return newPassword;
            }
            else
                throw new ProviderException(Messages.msgPasswordFormatIncorrect);

            throw new ProviderException(Messages.msgFailToResetPassword);
        }

        /// <summary>
        /// Выполняет проверку пользователя 
        /// </summary>
        /// <remarks>
        /// Проверяется правильность форматов, существование , блокировка и подтверждение пользователя</remarks>
        /// <param name="username">
        /// Имя пользователя
        /// </param>
        /// <param name="password">
        /// Пароль
        /// </param>
        /// <returns>
        ///  True, если проверка прошла успешно
        /// </returns>
        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            if (!this.VerifyPasswordIsValid(password))
                return false;

            var user = this.GetUser(username, false);
            if (user == null)
                return false;

            if (!user.IsApproved || user.IsLockedOut)
                return false;

            var res = this._adapter.TryCheckUserPassword(user, password);
            if (res)
                _adapter.UpdateUserActivityDate(user);
            return res;
        }

        /// <summary>
        /// Проверяет правильность формата пароля согласно указанным в настрйоках параметрам
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>True, если пароль соответствует политике безопасности</returns>
        protected virtual bool VerifyPasswordIsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            if (!(password.Length >= this.MinRequiredPasswordLength))
                return false;

            if (password.Count(c => (!char.IsDigit(c) && !char.IsLetter(c)))
                < this._minRequiredNonAlphanumericCharacters)
                return false;

            var regex = new Regex(this.PasswordStrengthRegularExpression, RegexOptions.Compiled);
            if (regex.Matches(password).Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <returns>
        /// True if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        /// <param name="userName">
        /// The membership user whose lock status you want to clear.
        /// </param>
        public override bool UnlockUser(string userName)
        {
            var user = GetUser(userName, false);
            if (user == null)
                return false;
            return this._adapter.UnlockUser(user);
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="providerUserKey">
        /// The unique identifier for the membership user to get information for.
        /// </param>
        /// <param name="userIsOnline">
        /// True to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.
        /// </param>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
                throw new ArgumentNullException("providerUserKey");

            var user = this._adapter.GetUser(providerUserKey);
            if (user != null && userIsOnline)
                this._adapter.UpdateUserActivityDate(user);
            return user;
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">
        /// The name of the user to get information for. 
        /// </param>
        /// <param name="userIsOnline">
        /// True to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. 
        /// </param>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            var user = this._adapter.GetUser(username);
            if (user != null && userIsOnline)
                this._adapter.UpdateUserActivityDate(user);
            return user;
        }

        /// <summary>
        /// Получает имя пользователя по e-mail
        /// </summary>
        /// <returns>
        /// Имя пользователя с соответствующим e-mail. null, если ничего не найдено.
        /// </returns>
        /// <param name="email">Адрес почты</param>
        public override string GetUserNameByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            if (!this.RequiresUniqueEmail)
                throw new NotSupportedException(Messages.msgNeedUniqueEMail);

            int maxCount;
            var users = this._adapter.FindUsersByEmail(email, 0, int.MaxValue, out maxCount);
            foreach (MembershipUser user in users)
            {
                if (user.Email.Contains(email))
                    return user.UserName;
            }

            return null;
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            int totalCount;
            return this._adapter.GetAllUsers(0, int.MaxValue, out totalCount).OfType<MembershipUser>().Count(u => u.IsOnline);
        }

        /// <summary>
        /// Removes a user from the membership data source. 
        /// </summary>
        /// <returns>
        /// True if the user was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="username">
        /// The name of the user to delete.
        /// </param>
        /// <param name="deleteAllRelatedData">
        /// True to delete data related to the user from the database; false to leave data related to the user in the database.
        /// </param>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            // ищем запись пользователя
            var user = GetUser(username, false);
            if (user == null)
                throw new ProviderException(Messages.msgUserNotFound);

            return this._adapter.DeleteUser(user, deleteAllRelatedData);
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the information for the newly created user.
        /// </returns>
        /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><param name="email">The e-mail address for the new user.</param><param name="passwordQuestion">The password question for the new user.</param><param name="passwordAnswer">The password answer for the new user</param><param name="isApproved">Whether or not the new user is approved to be validated.</param><param name="providerUserKey">The unique identifier from the membership data source for the user.</param><param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
        public override MembershipUser CreateUser(
            string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(passwordQuestion))
                throw new ArgumentNullException("passwordQuestion", Messages.msgNeedSecretQuestion);
            if (this.RequiresQuestionAndAnswer && string.IsNullOrWhiteSpace(passwordAnswer))
                throw new ArgumentNullException("passwordAnswer", Messages.msgNeedSecretAnswer);
            return this._adapter.CreateUser(
                username,
                password,
                email,
                passwordQuestion,
                passwordAnswer,
                isApproved,
                providerUserKey,
                out status);
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents the user to update and the updated information for the user. </param>
        public override void UpdateUser(MembershipUser user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            _adapter.UpdateUser(user);
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="emailToMatch">The e-mail address to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _adapter.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="usernameToMatch">
        /// The user name to search for.
        /// </param>
        /// <param name="pageIndex">
        /// The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.
        /// </param>
        /// <param name="pageSize">
        /// The size of the page of results to return.
        /// </param>
        /// <param name="totalRecords">
        /// The total number of matched users.
        /// </param>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return _adapter.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return _adapter.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }
        #endregion
    }
}
