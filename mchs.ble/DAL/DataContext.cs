namespace DAL
{
    using Init.DbCore.DB.MsSql;

    /// <summary>
    /// Контекст доступа к БД
    /// </summary>
    public class DataContext : MsSqlCoreDb
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">
        /// Строка подключения
        /// </param>
        public DataContext(string connectionString)
            : base(connectionString)
        {
            new MsSqlCoreDb(connectionString);
        }
    }
}
