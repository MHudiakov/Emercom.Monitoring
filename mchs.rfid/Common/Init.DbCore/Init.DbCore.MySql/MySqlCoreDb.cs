// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MySqlCoreDb.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Обертка над SqlConnection. для организации доcтупа к БД через пул соединений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.MySql
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;

    using Init.DbCore.DB;

    using global::MySql.Data.MySqlClient;

    /// <summary>
    /// Обертка над SqlConnection. для организации доcтупа к БД через пул соединений
    /// </summary>
    public class MySqlCoreDb : CoreDbBase
    {
        /// <summary>
        /// Создать подключение к БД
        /// </summary>
        /// <returns>SqlConnection подключение к бд</returns>
        public override DbConnection CreateConnection()
        {
            return new MySqlConnection(this.ConnectionString);
        }

        /// <summary>
        /// Фабричный метод создания адаптера
        /// </summary>
        /// <returns>Адаптер соотвесвуюущий используемой инфораструктуре</returns>
        protected override DbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        /// <summary>
        /// Обертка над SqlConnection. для организации доатса к БД через пул с транзакциями
        /// </summary>
        /// <param name="connectionString">
        /// Строка подключения к бд
        /// </param>
        public MySqlCoreDb(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Получить скаляр
        /// </summary>
        /// <param name="sql">
        /// Текст запроса
        /// </param>
        /// <param name="transaction">
        /// Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="commandType">
        /// Тип команды
        /// </param>
        /// <param name="args">
        /// Параметр
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
                command.CommandType = commandType;
                if (transaction != null)
                    command.Transaction = transaction;
                command.Parameters.AddRange(args.Select(p => new MySqlParameter("@" + p.ParameterName.Trim('@'), p.Value)).ToArray());
                command.CommandText = sql;
                return command.ExecuteScalar();
            }
            finally
            {
                if (transaction == null)
                    connection.Close();
            }
        }

        /// <summary>
        /// Получить таблицу
        /// </summary>
        /// <param name="sql">
        /// Запрос
        /// </param>
        /// <param name="transaction">Транзакция запроса (соединение не будет автоматически закрыто)</param>
        /// <param name="args">
        /// Параметры
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

                if (transaction != null)
                    command.Transaction = transaction;

                command.CommandText = sql;

                if (args.Length != 0)
                    command.Parameters.AddRange(args.ToArray());

                var adapter = new MySqlDataAdapter((MySqlCommand)command);
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

        /// <summary>
        /// Добавить в таблицу
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <param name="transaction">
        /// Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="returnIdentity">
        /// True, если нужно вернуть колонку Identity 
        /// </param>
        /// <param name="insertIdentity">
        /// The insert Identity.
        /// </param>
        /// <param name="args">
        /// Колонки для вставки
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
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
                                      string.IsNullOrEmpty(next) ? string.Empty : string.Format("{0}", next)));

            var paramNames = args.Select(item => item.ParameterName)
                                 .Aggregate(
                                     string.Empty,
                                     (current, next) =>
                                     string.Format(
                                         "{0} {1}",
                                         string.IsNullOrEmpty(current) ? string.Empty : string.Format("{0},", current),
                                         string.IsNullOrEmpty(next) ? string.Empty : string.Format("@{0}", next.Trim('@'))));

            var sql = string.Format("Insert Into {0} ({1}) Values({2}); Select LAST_INSERT_ID();", tableName, columns, paramNames);
            if (!returnIdentity)
                sql = string.Format("Insert Into {0} ({1}) Values({2});", tableName, columns, paramNames);

            return this.ExecuteScalar(sql, transaction, args.ToArray());
        }

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        /// <param name="tableName">
        /// Имя таблицы
        /// </param>
        /// <param name="keyColumns">
        /// Ключевые столбцы
        /// </param>
        /// <param name="transaction">Транзакция запроса (соединение не будет автоматически закрыто)</param>
        /// <param name="args">
        /// Колонки для вставки
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

            string sql = string.Format("Update {0} Set {1} Where {2}", tableName, paramNames, sqlWhere);

            var newArgs = new List<DbParameter>(args);

            // todo: работа с ключевыми полями
            foreach (var keyColumn in keyColumns)
                if (!newArgs.Exists(p => p.ParameterName == keyColumn.ParameterName))
                    newArgs.AddRange(keyColumns);

            this.ExecuteScalar(sql, transaction, newArgs.ToArray());
        }
    }
}
