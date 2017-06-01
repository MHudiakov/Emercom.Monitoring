// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StandartDataPointStrategy.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.GeneralDataPoint
{
    using System;
    using System.Collections.Generic;

    using Init.DAL.Sync.Transfer;
    using Init.DbCore;
    using Init.DbCore.DataAccess;

    /// <summary>
    /// Стандартная стратегия для реализации интерфейса <see cref="IGeneralDataPoint"/> 
    /// </summary>
    public class StandartDataPointStrategy : IGeneralDataPoint
    {
        /// <summary>
        /// Список адаптеров шлюзов посредством которых
        /// можно выполнять операции над данными в обобщенном виде
        /// </summary>
        private readonly Dictionary<string, IGeneralDataPoint> _dataPoints = new Dictionary<string, IGeneralDataPoint>();

        /// <summary>
        /// Регистрирует шлюз доступа к данным
        /// в стратегии универсальной точки обмена данными
        /// </summary>
        /// <typeparam name="T">
        /// Тип шлюза записи
        /// </typeparam>
        /// <param name="dataAccess">
        /// Шлюз таблицы данных
        /// </param>
        public void RegisterDataAccess<T>(DataAccess<T> dataAccess) where T : DbObject
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");

            var adapter = new DataAccessAdapter<T>(dataAccess);

            if (_dataPoints.ContainsKey(adapter.TypeName))
                throw new ArgumentException(
                    string.Format("Шлюз таблицы для объектов {0} уже зарегистрирован.", adapter.TypeName));

            _dataPoints.Add(adapter.TypeName, adapter);
        }

        /// <summary>
        /// Получить количество элементов
        /// </summary>
        /// <param name="typeName">
        /// Имя шлюза записи
        /// </param>
        /// <returns>
        /// Количество элементов, лоступное через шлюз записи
        /// </returns>
        public long Count(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException("typeName");

            if (!_dataPoints.ContainsKey(typeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", typeName), typeName);

            return _dataPoints[typeName].Count(typeName);
        }

        /// <summary>
        /// Добавляет запись
        /// </summary>
        /// <param name="item">
        /// Информация о добавляемой записи
        /// </param>
        /// <returns>
        /// Идентификатор добавленной записи
        /// </returns>
        public Change Add(Change item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!_dataPoints.ContainsKey(item.TypeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", item.TypeName), "item");

            return _dataPoints[item.TypeName].Add(item);
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        /// <returns>
        ///  Измененный объект (Возврат изменений сервера)
        /// </returns>
        public Change Edit(Change item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!_dataPoints.ContainsKey(item.TypeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", item.TypeName), "item");

           return _dataPoints[item.TypeName].Edit(item);
        }

        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="item">
        /// Новое состояние записи
        /// </param>
        public void DeleteWhere(Change item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (!_dataPoints.ContainsKey(item.TypeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", item.TypeName), "item");

            _dataPoints[item.TypeName].DeleteWhere(item);
        }

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
        public List<Change> GetItemsWhere(string typeName, Dictionary<string, object> keys)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException("typeName");

            if (!_dataPoints.ContainsKey(typeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", typeName), typeName);

            return _dataPoints[typeName].GetItemsWhere(typeName, keys);
        }

        /// <summary>
        /// Получение данных посредством TransferManager
        /// </summary>
        /// <param name="typeName">Имя шлюза записи</param>
        /// <param name="ident">Идентификатор части</param>
        /// <returns>Часть данных со списка</returns>
        public TransferPart TransferItems(string typeName, TransferPartIdent ident)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException("typeName");

            if (!_dataPoints.ContainsKey(typeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", typeName), typeName);

            return _dataPoints[typeName].TransferItems(typeName, ident);
        }

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
        public List<Change> GetPage(string typeName, int pageIndex, int count)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException("typeName");

            if (!_dataPoints.ContainsKey(typeName))
                throw new ArgumentException(string.Format("Шлюз тиблицы {0} не зарегистрирован", typeName), typeName);
            return _dataPoints[typeName].GetPage(typeName, pageIndex, count);
        }
    }
}