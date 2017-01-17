// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbBacup.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс - инструмент для выполнения bacup/restore
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.MsSql.Tools
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    /// <summary>
    /// Класс - инструмент для выполнения bacup/restore MsSQL
    /// </summary>
    public class DbBacup
    {
        #region Запросы к бд
        /// <summary>
        /// Запрос закрытия подключений к БД MsSQL
        /// </summary>
        private const string CLOSE_CONNECTIONS_TO_DB =
            @"USE master 
            SET NOCOUNT ON 
            DECLARE @spidstr varchar(8000) 
            DECLARE @ConnKilled smallint 
            SET @ConnKilled=0 
            SET @spidstr = '' 
            
            SELECT @spidstr=coalesce(@spidstr,',' )+'kill '+convert(varchar, spid)+ '; ' 
            FROM master..sysprocesses WHERE dbid=db_id(@databaseName) 
             
            IF LEN(@spidstr) > 0 
            BEGIN 
            EXEC(@spidstr) 
            SELECT @ConnKilled = COUNT(1) 
            FROM master..sysprocesses WHERE dbid=db_id(@databaseName) 
            END; ";

        /// <summary>
        /// Запрос получения пути к файлам БД
        /// </summary>
        private const string GET_DATA_PATH_QUERY = @"use master
                        (SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)
                                          FROM master.sys.master_files
                                          WHERE database_id = 1 AND file_id = 1); ";

        /// <summary>
        /// Запрос создания БД
        /// </summary>
        private const string CREATE_QUERY = @"use master
                        RESTORE DATABASE @databaseName
                          FROM disk = @databaseBackupFileName
                          WITH RECOVERY,
                          MOVE @databaseDataFile TO @databaseDataFileName,
                          MOVE @databaseLogFile TO @databaseLogFileName,
                          norewind,
                          nounload,
                        replace; ";

        /// <summary>
        /// Запрос выполнения бекапа БД
        /// </summary>
        private const string BACUP_QUERY = @"BACKUP DATABASE @databaseName TO DISK = @backupFileName";
        #endregion

        /// <summary>
        /// Выполняет бекап БД в указанный файл
        /// </summary>
        /// <param name="connection">
        /// Подключение к БД
        /// </param>
        /// <param name="dbName">
        /// Название БД
        /// </param>
        /// <param name="backupFileName">
        /// Файл бекапа
        /// </param>
        public void Bacup(MsSqlCoreDb connection, string dbName, string backupFileName)
        {
            try
            {
                connection.ExecuteScalar(BACUP_QUERY, new SqlParameter("@databaseName", dbName), new SqlParameter("@backupFileName", backupFileName));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Ошибка аоздания резервной копии базы данных: {0} в файл: {1}", dbName, backupFileName), ex);
            }
        }

        /// <summary>
        /// Восстанавливает бекап в новую БД
        /// </summary>
        /// <param name="connection">
        /// Соединения для восстановления БД
        /// </param>
        /// <param name="dbName">
        /// Название новой бд
        /// </param>
        /// <param name="backupFileName">
        /// Файл бекапа
        /// </param>
        public void Restore(MsSqlCoreDb connection, string dbName, string backupFileName)
        {
            var info = connection.ExecuteDataTable(string.Format(@"RESTORE FILELISTONLY FROM DISK = '{0}'", backupFileName));

            var dataFile = info.Rows[0][0];
            var logFile = info.Rows[1][0];

            var dataPath = (string)connection.ExecuteScalar(GET_DATA_PATH_QUERY);

            var dbDataFileName = Path.Combine(dataPath, dbName + ".mdf");
            var dbLogFileName = Path.Combine(dataPath, dbName + "_log.ldf");

            connection.ExecuteScalar(
                CLOSE_CONNECTIONS_TO_DB + CREATE_QUERY, 
                new SqlParameter("databaseName", string.Format("{0}", dbName)), 
                new SqlParameter("databaseBackupFileName", backupFileName),
                new SqlParameter("databaseDataFile", dataFile),
                new SqlParameter("databaseLogFile", logFile),
                new SqlParameter("databaseDataFileName", dbDataFileName),
                new SqlParameter("databaseLogFileName", dbLogFileName));
        }
    }

}
