// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataAccessAdapter.cs" company="ИНИТ-центр">
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
    using System.Linq;

    using Init.DAL.Sync.Common;
    using Init.DAL.Sync.Transfer;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Адаптер шлюза таблицы для универсальной точки обмена данными
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    internal class DataAccessAdapter<T> : IGeneralDataPoint
        where T : DbObject
    {
        /// <summary>
        /// Размер блока при передаче списка
        /// </summary>
        private const int PART_SIZE = 500;

        /// <summary>
        /// Адаптируемый шлюз таблицы
        /// </summary>
        private readonly DataAccess<T> _dataAccess;

        /// <summary>
        /// Менеджер передачи больших списков
        /// </summary>
        private readonly TransferManager _transferManager = new TransferManager();

        /// <summary>
        /// Метеданные объекта T
        /// </summary>
        private readonly DbObjectMetadata _metadata = new DbObjectMetadata(typeof(T));

        /// <summary>
        /// Название шлюза записи
        /// </summary>
        public string TypeName { get; private set; }


        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DataAccessAdapter{T}"/>. 
        /// Адаптер шлюза таблицы для универсальной точки обмена данными
        /// </summary>
        /// <param name="dataAccess">
        /// Адаптируемый шлюз таблицы
        /// </param>
        public DataAccessAdapter(DataAccess<T> dataAccess)
        {
            if (dataAccess == null)
                throw new ArgumentNullException("dataAccess");

            TypeName = typeof(T).Name;
            _dataAccess = dataAccess;
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
            if (typeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", typeName), "typeName");

            return this._dataAccess.GetCount();
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
            if (item.ChangeType != ChangeTypeEnum.Add)
                throw new ArgumentException(string.Format("Неожиданный тип характера изменений item.ChangeType ({0}). Ожидпался {1}", item.ChangeType, ChangeTypeEnum.Add));
            if (item.TypeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", item.TypeName), "item");
            if (_metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            var obj = item.GetItem<T>();
            _dataAccess.Add(obj);

            return new Change(ChangeTypeEnum.Edit, item.TypeName, DateTime.Now, new Dictionary<string, object> { { _metadata.Key.PropertyInfo.Name, obj.KeyValue } }, obj);
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
            if (item.ChangeType != ChangeTypeEnum.Edit)
                throw new ArgumentException(string.Format("Неожиданный тип характера изменений item.ChangeType ({0}). Ожидпался {1}", item.ChangeType, ChangeTypeEnum.Edit));
            if (item.TypeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", item.TypeName), "item");
            if (_metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            var obj = item.GetItem<T>();
            _dataAccess.Edit(obj);

            return new Change(ChangeTypeEnum.Edit, item.TypeName, DateTime.Now, new Dictionary<string, object> { { _metadata.Key.PropertyInfo.Name, obj.KeyValue } }, obj);
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
            if (item.ChangeType != ChangeTypeEnum.Delete)
                throw new ArgumentException(string.Format("Неожиданный тип характера изменений item.ChangeType ({0}). Ожидпался {1}", item.ChangeType, ChangeTypeEnum.Delete));
            if (item.TypeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", item.TypeName), "item");
            if (_metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            _dataAccess.DeleteWhere(item.GetFilterProps());
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
            if (typeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", typeName), "typeName");
            if (keys == null)
                throw new ArgumentNullException("keys");

            var items = _dataAccess.GetItemsWhere(keys);
            return items.Select(e => new Change(ChangeTypeEnum.Add, TypeName, DateTime.Now, keys, e)).ToList();
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
            if (typeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", typeName), "typeName");

            return _transferManager.ReturnPart(ident, PART_SIZE, () => this._dataAccess.GetAll().ToList());
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
            if (typeName != TypeName)
                throw new ArgumentException(string.Format("Стратегия не может выполнять операции для шлюза с именем {0}", typeName), "typeName");
            if (_metadata.Key == null)
                throw new NotSupportedException(string.Format("Объект {0} не имеет ключевого поля. Возможно Вы забыли отметить его атрибутом [DbKey]", typeof(T).FullName));

            return _dataAccess.GetPage(pageIndex, count).Select(e => new Change(ChangeTypeEnum.Add, TypeName, DateTime.Now, new Dictionary<string, object> { { _metadata.Key.PropertyInfo.Name, e.KeyValue } }, e)).ToList();
        }
    }
}