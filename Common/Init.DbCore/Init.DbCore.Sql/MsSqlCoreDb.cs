// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MsSqlCoreDb.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Обертка над SqlConnection. для организации доcтупа к БД через пул соединений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;

    using Init.DbCore.DB;

    /// <summary>
    /// Обертка над SqlConnection. для организации доcтупа к БД через пул соединений
    /// </summary>
    public class MsSqlCoreDb : CoreDbBase
    {
        /// <summary>
        /// Обертка над SqlConnection. для организации доатса к БД через пул с транзакциями
        /// </summary>
        /// <param name="connectionString">
        /// Строка подключения к бд
        /// </param>
        public MsSqlCoreDb(string connectionString)
            : base(connectionString)
        {
            this.CommandTimeOut = 30;
        }

        /// <summary>
        /// Таймаут выполнения запроса к базе
        /// </summary>
        public int CommandTimeOut { get; set; }

        /// <summary>
        /// Фабричный метод создания адаптера
        /// </summary>
        /// <returns>Адаптер соотвесвуюущий используемой инфораструктуре</returns>
        protected override DbDataAdapter CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }

        #region ExecuteScalar
        /// <summary>
        /// Получить скаляр
        /// </summary>
        /// <param name="sql">
        ///     Текст запроса
        /// </param>
        /// <param name="transaction">
        ///     Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="commandType">
        ///     Тип команды
        /// </param>
        /// <param name="args">
        ///     Параметр
        /// </param>
        /// <returns>
        /// Результат запроса
        /// </returns>
        public override object ExecuteScalar(string sql, DbTransaction transaction, CommandType commandType, params DbParameter[] args)
        {
            var connection = transaction != null ? transaction.Connection : this.CreateConnection();
            try
            {
                if (transaction == null)
                    connection.Open();
                var command = connection.CreateCommand();
                command.CommandTimeout = this.CommandTimeOut;
                command.CommandType = commandType;
                if (transaction != null)
                    command.Transaction = transaction;
                command.Parameters.AddRange(args.Select(p => new SqlParameter("@" + p.ParameterName.Trim('@'), p.Value)).ToArray());

                command.CommandText = sql;
                return command.ExecuteScalar();
            }
            finally
            {
                if (transaction == null)
                    connection.Close();
            }
        }
        #endregion

        #region ExecuteDataTable
        /// <summary>
        /// Получить таблицу
        /// </summary>
        /// <param name="sql">
        ///     Запрос
        /// </param>
        /// <param name="transaction">Транзакция запроса (соединение не будет автоматически закрыто)</param>
        /// <param name="args">
        ///     Параметры
        /// </param>
        /// <returns>
        /// Таблица с данными
        /// </returns>
        public override DataTable ExecuteDataTable(string sql, DbTransaction transaction, params DbParameter[] args)
        {
            var connection = transaction != null ? transaction.Connection : this.CreateConnection();
            try
            {
                if (transaction == null)
                    connection.Open();
                var command = connection.CreateCommand();
                command.CommandTimeout = this.CommandTimeOut;
                if (transaction != null)
                    command.Transaction = transaction;
                command.CommandText = sql;
                command.Parameters.AddRange(args.Select(p => new SqlParameter("@" + p.ParameterName.Trim('@'), p.Value ?? DBNull.Value)).ToArray());
                var adapter = this.CreateDataAdapter();
                adapter.SelectCommand = command;
                var result = new DataTable();
                adapter.Fill(result);
                return result;
            }
            finally
            {
                if (transaction == null)
                    connection.Close();
            }
        }
        #endregion

        #region InsertIntoTable
        /// <summary>
        /// Добавить в таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="transaction">
        ///     Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="returnIdentity">
        ///     True, если нужно вернуть колонку Identity 
        /// </param>
        /// <param name="insertIdentity">
        ///     Флаг: взвращать автогенерированное поле
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        /// <returns>
        ///     Идентификатор добавленной записи
        /// </returns>
        public override object InsertIntoTable(string tableName, DbTransaction transaction, bool returnIdentity, bool insertIdentity, params DbParameter[] args)
        {
            var columns = args.Select(item => item.ParameterName)
                              .Aggregate(
                                  string.Empty,
                                  (current, next) =>
                                  string.Format(
                                      "{0} {1}",
                                      string.IsNullOrEmpty(current) ? string.Empty : string.Format("{0},", current),
                                      string.IsNullOrEmpty(next) ? string.Empty : string.Format("[{0}]", next)));

            var paramNames = args.Select(item => item.ParameterName)
                                 .Aggregate(
                                     string.Empty,
                                     (current, next) =>
                                     string.Format(
                                         "{0} {1}",
                                         string.IsNullOrEmpty(current) ? string.Empty : string.Format("{0},", current),
                                         string.IsNullOrEmpty(next) ? string.Empty : string.Format("@{0}", next.Trim('@'))));

            var sql = string.Format("Insert Into [{0}] ({1}) Values({2}); SELECT SCOPE_IDENTITY();", tableName, columns, paramNames);
            if (!returnIdentity)
                sql = string.Format("Insert Into [{0}] ({1}) Values({2});", tableName, columns, paramNames);

            if (insertIdentity)
                sql = string.Format("SET IDENTITY_INSERT [{0}] ON; {1} SET IDENTITY_INSERT [{0}] OFF;", tableName, sql);

            return this.ExecuteScalar(sql, transaction, args.ToArray());
        }
        #endregion

        #region UpdateTable
        /// <summary>
        /// Обновить таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="keyColumns">
        ///     Ключевые столбцы
        /// </param>
        /// <param name="transaction">Транзакция запроса (соединение не будет автоматически закрыто)</param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        public override void UpdateTable(string tableName, List<DbParameter> keyColumns, DbTransaction transaction, params DbParameter[] args)
        {
            var paramNames = args.Select(item => item.ParameterName)
                .Aggregate(
                string.Empty,
                (current, next) => string.Format(
                    "{0} {1}",
                    string.IsNullOrEmpty(current) ? string.Empty : string.Format("{0},", current),
                    string.IsNullOrEmpty(next) ? string.Empty : string.Format("{0}= @{0}", next.Trim('@'))));

            string sqlWhere = string.Empty;
            foreach (var par in keyColumns)
            {
                if (!string.IsNullOrEmpty(sqlWhere)) sqlWhere += " AND ";
                sqlWhere += string.Format("{0}=@{0}", par.ParameterName.Trim('@'));
            }

            string sql = string.Format("Update [{0}] Set {1} Where {2}", tableName, paramNames, sqlWhere);

            var newArgs = new List<DbParameter>(args);
            newArgs.AddRange(keyColumns);

            this.ExecuteScalar(sql, transaction, newArgs.OfType<DbParameter>().ToArray());
        }
        #endregion

        #region ExecuteBulkCopy
        /// <summary>
        /// Выполнить разовую вставку нескольких объектов в таблицу базы данных
        /// </summary>
        /// <param name="dataTable">
        /// Представление/описание таблицы. 
        /// Название таблицы и название столбцов в описании должно совпадать с
        /// названием таблицы и названиями столбцов в базе данных соответственно
        /// </param>
        /// <param name="destTableName">Имя целевой таблицы</param>
        public void ExecuteBulkCopy(DataTable dataTable, string destTableName)
        {
            if (dataTable == null)
                throw new ArgumentNullException("dataTable");

            if (string.IsNullOrWhiteSpace(destTableName))
                throw new ArgumentNullException("destTableName");

            var connection = (SqlConnection)this.CreateConnection();
            try
            {
                using (var bulkCopy = new SqlBulkCopy(connection))
                {
                    // Сопостовляем столбцы таблицы SQL Server и DataTable
                    foreach (DataColumn column in dataTable.Columns)
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));

                    // Указываем название таблицы в которуе делаем вставку
                    bulkCopy.DestinationTableName = destTableName;

                    // Передаём SqlBulkCopy представление таблицы (DataTable) и делаем запись данных в базу
                    bulkCopy.WriteToServer(dataTable);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion

        /// <summary>
        /// Создать подключение к БД
        /// </summary>
        /// <returns>SqlConnection подключение к бд</returns>
        public override DbConnection CreateConnection()
        {
            var connection = new SqlConnection(this.ConnectionString);
            return connection;
        }
    }
}
