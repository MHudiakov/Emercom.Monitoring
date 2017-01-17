// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreDbBase.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Обертка над SqlConnection. для организации доcтупа к БД через пул соединений
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DbCore.DB
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    /// <summary>
    /// Базовый класс управления подключением к БД
    /// </summary>
    public abstract class CoreDbBase
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Обертка над SqlConnection. для организации доатса к БД через пул с транзакциями
        /// </summary>
        /// <param name="connectionString">
        /// Строка подключения к бд
        /// </param>
        protected CoreDbBase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            this.ConnectionString = connectionString;
        }

        /// <summary>
        /// Создать подключение к БД
        /// </summary>
        /// <returns>SqlConnection подключение к бд</returns>
        public abstract DbConnection CreateConnection();

        /// <summary>
        /// Фабричный метод создания адаптера
        /// </summary>
        /// <returns>Адаптер соотвесвуюущий используемой инфораструктуре</returns>
        protected abstract DbDataAdapter CreateDataAdapter();

        /// <summary>
        /// Получить скаляр
        /// </summary>
        /// <param name="sql">
        ///     Текст запроса
        /// </param>
        /// <param name="args">
        ///     Параметр
        /// </param>
        /// <returns>
        /// Результат запроса
        /// </returns>
        public object ExecuteScalar(string sql, params DbParameter[] args)
        {
            return this.ExecuteScalar(sql, null, CommandType.Text, args);
        }

        /// <summary>
        /// Получить скаляр
        /// </summary>
        /// <param name="sql">
        ///     Текст запроса
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
        public object ExecuteScalar(string sql, CommandType commandType, params DbParameter[] args)
        {
            return this.ExecuteScalar(sql, null, commandType, args);
        }

        /// <summary>
        /// Получить скаляр
        /// </summary>
        /// <param name="sql">
        ///     Текст запроса
        /// </param>
        /// <param name="transaction">
        ///     Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="args">
        ///     Параметр
        /// </param>
        /// <returns>
        /// Результат запроса
        /// </returns>
        public object ExecuteScalar(string sql, DbTransaction transaction, params DbParameter[] args)
        {
            return this.ExecuteScalar(sql, transaction, CommandType.Text, args);
        }

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
        public abstract object ExecuteScalar(string sql, DbTransaction transaction, CommandType commandType, params DbParameter[] args);

        /// <summary>
        /// Получить таблицу
        /// </summary>
        /// <param name="sql">
        ///     Запрос
        /// </param>
        /// <param name="args">
        ///     Параметры
        /// </param>
        /// <returns>
        /// Таблица с данными
        /// </returns>
        public DataTable ExecuteDataTable(string sql, params DbParameter[] args)
        {
            return this.ExecuteDataTable(sql, null, args);
        }

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
        public abstract DataTable ExecuteDataTable(string sql, DbTransaction transaction, params DbParameter[] args);

        /// <summary>
        /// Добавить в таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="transaction">Транзакция запроса (соединение не будет автоматически закрыто)</param>
        /// <param name="returnIdentity">
        ///     True, если нужно вернуть колонку Identity 
        /// </param>
        /// <param name="insertIdentity">
        ///     Флаг: true, если втавляется столбец Identity
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
        /// </returns>
        public abstract object InsertIntoTable(string tableName, DbTransaction transaction, bool returnIdentity, bool insertIdentity, params DbParameter[] args);

        /// <summary>
        /// Добавить в таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
        /// </returns>
        public object InsertIntoTable(string tableName, params DbParameter[] args)
        {
            return this.InsertIntoTable(tableName, null, args);
        }

        /// <summary>
        /// Добавить в таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="transaction">
        ///     Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
        /// </returns>
        public object InsertIntoTable(string tableName, DbTransaction transaction, params DbParameter[] args)
        {
            return this.InsertIntoTable(tableName, transaction, true, false, args);
        }

        /// <summary>
        /// Добавить в таблицу с ИД
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="transaction">
        ///     Транзакция запроса (соединение не будет автоматически закрыто)
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        public void IdentityInsertIntoTable(string tableName, DbTransaction transaction, params DbParameter[] args)
        {
            this.InsertIntoTable(tableName, transaction, false, true, args);
        }

        /// <summary>
        /// Добавить в таблицу с ИД
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        public void IdentityInsertIntoTable(string tableName, params DbParameter[] args)
        {
            this.InsertIntoTable(tableName, null, false, true, args);
        }

        /// <summary>
        /// Обновить таблицу
        /// </summary>
        /// <param name="tableName">
        ///     Имя таблицы
        /// </param>
        /// <param name="keyColumns">
        ///     Ключевые столбцы
        /// </param>
        /// <param name="args">
        ///     Колонки для вставки
        /// </param>
        public void UpdateTable(string tableName, List<DbParameter> keyColumns, params DbParameter[] args)
        {
            this.UpdateTable(tableName, keyColumns, null, args);
        }

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
        public abstract void UpdateTable(string tableName, List<DbParameter> keyColumns, DbTransaction transaction, params DbParameter[] args);
    }
}
