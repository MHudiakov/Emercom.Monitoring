// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DalDataContext.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2016г.
// </copyright>
// <summary>
//   Контекст доступа к БД
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DAL
{
    using Init.DbCore.DB.MsSql;

    /// <summary>
    /// Контекст доступа к БД
    /// </summary>
    public class DalDataContext : MsSqlCoreDb
    {
        /// <summary>
        /// Конструктор контекста
        /// </summary>
        /// <param name="connectionString">
        /// Строка подключения
        /// </param>
        public DalDataContext(string connectionString)
            : base(connectionString)
        {
            new MsSqlCoreDb(connectionString);
        }
    }
}
