// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DBUpdate.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс обновления метаданных БД
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB
{
    using System;
    using System.Data.Common;
    using System.Diagnostics.CodeAnalysis;

    using Init.Tools;

    /// <summary>
    /// Класс обновления метаданных БД
    /// </summary>
    public class DbUpdate
    {
        /// <summary>
        /// Контекст доступа к данным
        /// </summary>
        public CoreDbBase Context { get; private set; }

        /// <summary>
        ///  Класс обновления метаданных БД
        /// </summary>
        /// <param name="context">Контекст доступа к данным</param>
        public DbUpdate(CoreDbBase context)
        {
            this.Context = context;
            this.Major = 0;
            this.Minor = 0;
            this.Logger = new Loger();
        }

        /// <summary>
        /// Текущая версия
        /// </summary>
        public int Major { get; private set; }

        /// <summary>
        /// Текущий билд
        /// </summary>
        public int Minor { get; private set; }

        /// <summary>
        /// Обертка системы логирования
        /// </summary>
        public Loger Logger { get; private set; }

        /// <summary>
        /// Выполняет обновление с текущей ревизии до указанной
        /// </summary>
        /// <param name="current">Текущая версия</param>
        /// <param name="to">Версия посл еобновления</param>
        /// <param name="doUpdate">Функция обновления БД</param>
        protected void Update(Version current, Version to, Action<DbTransaction> doUpdate)
        {
            if (this.Major == current.Major && this.Minor == current.Minor)
            {
                var connection = this.Context.CreateConnection();
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    this.Logger.LogMsg(string.Format("Подьем БД до v{0}.{1}", to.Major, to.Minor));
                    doUpdate(transaction);
                    this.Context.ExecuteScalar("Update dbVersion set Major=" + to.Major + ", Minor=" + to.Minor, transaction);
                    transaction.Commit();
                    this.Major = to.Major;
                    this.Minor = to.Minor;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(string.Format("Ошибка при подъеме БД до v{0}.{1}", to.Major, to.Minor), ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        /// <summary>
        /// Выполняет обновление метаданных БД
        /// </summary>
        [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:DoNotPlaceRegionsWithinElements",
            Justification = "Reviewed. Suppression is OK here.")]
        public virtual void UpdateMetadata()
        {
            try
            {
                this.Major = Convert.ToInt32(this.Context.ExecuteScalar("select Major From dbVersion"));
                this.Minor = Convert.ToInt32(this.Context.ExecuteScalar("select Minor From dbVersion"));
            }
            catch
            {
                this.Major = 0;
                this.Minor = 0;
            }

            this.Update(
                new Version(0, 0),
                new Version(1, 0),
                trans =>
                {
                    this.Context.ExecuteScalar(
                        "IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'dbversion') DROP TABLE [dbVersion];",
                        trans);
                    const string SQL = @"CREATE TABLE [dbo].[dbVersion](
                                    [Major] [int] NOT NULL,
                                    [Minor] [int] NOT NULL);";
                    this.Context.ExecuteScalar(SQL, trans);
                    this.Context.ExecuteScalar("Insert into dbVersion VALUES (1, 0);", trans);
                });
        }
    }

    [Flags]
    public enum IndexType
    {
        Unique=2,
        Clustered =4,
        Asc =8,
    }
}
