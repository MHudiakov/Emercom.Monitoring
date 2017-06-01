// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGeneralDataPoint.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.GeneralDataPoint
{
    using System.Collections.Generic;

    using Init.DAL.Sync.Transfer;

    /// <summary>
    /// Интерфейс универсальной точки обмена данными
    /// </summary>
    public interface IGeneralDataPoint
    {
        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <returns>
        /// Количество элементов, лоступное через шлюз записи
        /// </returns>
        long Count(string typeName);

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="item">
        /// Информация о добавляемой записи
        /// </param>
        /// <returns>
        /// Измененный объект, если есть автогенерируемые поля
        /// </returns>
        Change Add(Change item);

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        /// <returns>
        ///  Измененный объект (Возврат изменений сервера)
        /// </returns>
        Change Edit(Change item);

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        void DeleteWhere(Change item);

        /// <summary>
        /// Получение части записей по фильтру
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="keys">
        /// Улюч-фильтр
        /// </param>
        /// <returns>
        /// Часть объектов шлюза таблицы, проходищих фильтр keys
        /// </returns>
        List<Change> GetItemsWhere(string typeName, Dictionary<string, object> keys);

        /// <summary>
        /// Получение данных посредством TransferManager
        /// </summary>
        /// <param name="typeName">Имя шлюза записи</param>
        /// <param name="ident">Идентификатор части</param>
        /// <returns>Часть данных со списка</returns>
        TransferPart TransferItems(string typeName, TransferPartIdent ident);

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="count">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов начиная с idFrom длинной не более count
        /// </returns>
        List<Change> GetPage(string typeName, int pageIndex, int count);
    }
}