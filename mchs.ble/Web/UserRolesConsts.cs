namespace Web
{
    /// <summary>
    /// Константное строковое представление ролей.
    /// Используется для обозначения ролей в атрибуте 
    /// Authorize для каждого из контроллеров с ограниченным доступом
    /// </summary>
    public static class UserRolesConsts
    {
        /// <summary>
        /// User
        /// </summary>
        public const string User = "User";

        /// <summary>
        /// Administrator
        /// </summary>
        public const string Administrator = "Administrator";
    }
}