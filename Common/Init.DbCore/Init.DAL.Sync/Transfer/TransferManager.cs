// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TransferManager.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Менеджер пересылки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync.Transfer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Менеджер пересылки
    /// </summary>
    public class TransferManager
    {
        /// <summary>
        /// Время жизни ресурса
        /// </summary>
        private const int RESOURCE_LIFE_TIME = 10;

        #region EnumeratorWrapper
        /// <summary>
        /// Итератор с подсчетом позиции
        /// </summary>
        /// <typeparam name="T">Тип итератора</typeparam>
        internal class EnumeratorWrapper<T> : IEnumerator<T>
        {
            /// <summary>
            /// Исходный итератор
            /// </summary>
            private readonly IEnumerator<T> _enumerator;

            #region Pos
            /// <summary>
            /// Текущая позиция
            /// </summary>
            public int Pos
            {
                get
                {
                    return this._pos;
                }
            }

            /// <summary>
            /// Текущая позиция
            /// </summary>
            private int _pos;
            #endregion

            /// <summary>
            /// Итератор с подсчетом позиции
            /// </summary>
            /// <param name="enumerator">Исходный итератор</param>
            public EnumeratorWrapper(IEnumerator<T> enumerator)
            {
                if (enumerator == null)
                    throw new ArgumentNullException("enumerator");
                _enumerator = enumerator;
                _pos = -1;
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            public void Dispose()
            {
                _enumerator.Dispose();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// True if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">
            /// The collection was modified after the enumerator was created. 
            /// </exception>
            public bool MoveNext()
            {
                var res = _enumerator.MoveNext();
                if (res)
                    _pos++;
                return res;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception><filterpriority>2</filterpriority>
            public void Reset()
            {
                _enumerator.Reset();
                _pos = -1;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <returns>
            /// The element in the collection at the current position of the enumerator.
            /// </returns>
            public T Current
            {
                get
                {
                    return _enumerator.Current;
                }
            }

            /// <summary>
            /// Gets the current element in the collection.
            /// </summary>
            /// <returns>
            /// The current element in the collection.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
        }
        #endregion

        #region EnumerablelStateWrapper
        /// <summary>
        /// Обертка учитывающая результат предыдущих вызовов к IEnumerable
        /// </summary>
        /// <typeparam name="T">Тип IEnumerable</typeparam>
        internal class EnumerablelStateWrapper<T>
        {
            /// <summary>
            /// Исходная коллекци
            /// </summary>
            private readonly IEnumerable<T> _collection;

            /// <summary>
            /// Перечислитель коллекции
            /// </summary>
            private EnumeratorWrapper<T> _enumerator;

            /// <summary>
            /// Обертка учитывающая результат предыдущих вызовов к IEnumerable
            /// </summary>
            /// <param name="collection">Исходная коллекция</param>
            public EnumerablelStateWrapper(IEnumerable<T> collection)
            {
                if (collection == null)
                    throw new ArgumentNullException("collection");
                _collection = collection;
                _enumerator = new EnumeratorWrapper<T>(_collection.GetEnumerator());
            }

            /// <summary>
            /// Пропускает count элементов
            /// </summary>
            /// <param name="count">Количество элементов для пропуска</param>
            /// <returns>Обертка</returns>
            public EnumerablelStateWrapper<T> Skip(int count)
            {
                var countToSkip = count - this._enumerator.Pos - 1;
                if (countToSkip < 0)
                    _enumerator = new EnumeratorWrapper<T>(_collection.GetEnumerator());

                while (this._enumerator.Pos < count - 1)
                    if (!_enumerator.MoveNext())
                        break;

                return this;
            }

            /// <summary>
            /// Берет count элеметнтов
            /// </summary>
            /// <param name="count">Количество элементов</param>
            /// <returns>Коллекция элементов длинной не более count</returns>
            public IEnumerable<T> Take(int count)
            {
                var currentPos = this._enumerator.Pos;
                while (_enumerator.MoveNext())
                {
                    yield return _enumerator.Current;
                    if ((this._enumerator.Pos - currentPos) == count)
                        yield break;
                }
            }
        }
        #endregion

        /// <summary>
        /// Ресурс
        /// </summary>
        internal class TransferResource
        {
            /// <summary>
            /// Список частей для пересылки
            /// </summary>
            public EnumerablelStateWrapper<object> Items { get; private set; }

            /// <summary>
            /// Размер части пересылаемых данных
            /// </summary>
            public int PartSize { get; private set; }

            /// <summary>
            /// Время создания ресурса
            /// </summary>
            public DateTime DtCreated { get; private set; }

            /// <summary>
            /// Ресурс
            /// </summary>
            /// <param name="items">Список частей для пересылки</param>
            /// <param name="partSize">Размер части пересылаемых данных</param>
            public TransferResource(IEnumerable<object> items, int partSize)
            {
                if (items == null)
                    throw new ArgumentNullException("items");
                if (partSize <= 0)
                    throw new ArgumentException("Размер части должен быть положительным числом");

                Items = new EnumerablelStateWrapper<object>(items);
                PartSize = partSize;
                DtCreated = DateTime.Now;
            }
        }

        /// <summary>
        /// Счетчик ресурсов
        /// </summary>
        private int _resourceCounter;

        /// <summary>
        /// Семафор доступа к списку ресурсов
        /// </summary>
        private readonly object _resourcesLock = new object();

        /// <summary>
        /// Список ресурсов
        /// </summary>
        private readonly Dictionary<int, TransferResource> _resources = new Dictionary<int, TransferResource>();

        /// <summary>
        /// Возвращает часть часть пересылаемых данных ресурса
        /// </summary>
        /// <param name="id">Идентификатор ресурса</param>
        /// <param name="partNumber">Номер части</param>
        /// <returns>Часть пересылаемых данных ресурса</returns>
        private TransferPart GetPart(int id, int partNumber)
        {
            lock (_resourcesLock)
            {
                if (!_resources.ContainsKey(id))
                    throw new ArgumentException(string.Format("Не найден ресурс с идентификатором {0}", id), "id");

                var resource = _resources[id];

                var skiped = partNumber * resource.PartSize;

                var items = resource.Items.Skip(skiped).Take(resource.PartSize).ToList();

                var transferPartInfo = new TransferPartIdent(partNumber, id);

                var result = new TransferPart(transferPartInfo, items);
                if (items.Count < resource.PartSize)
                    result.IsLast = true;

                return result;
            }
        }

        /// <summary>
        /// Добавляет ресурс в список
        /// </summary>
        /// <param name="items">Список объектов для пересылки</param>
        /// <param name="partSize">Размер партии</param>
        /// <returns>Идентификатор ресурса</returns>
        public int AddResource(IEnumerable items, int partSize)
        {
            if (items == null)
                throw new ArgumentNullException("items");
            if (partSize <= 0)
                throw new ArgumentException(@"Размер части должен быть положительным числом", "partSize");

            this.RemoveOldResources();

            _resourceCounter++;
            var id = _resourceCounter;

            var resouce = new TransferResource(items.Cast<object>(), partSize);

            lock (_resourcesLock)
            {
                _resources.Add(id, resouce);
            }

            return id;
        }

        /// <summary>
        /// Удаляет ресурс из списка
        /// </summary>
        /// <param name="id">Идентификатор ресурса</param>
        public void RemoveResource(int id)
        {
            lock (_resourcesLock)
            {
                _resources.Remove(id);
            }
        }

        /// <summary>
        /// Проверяет содержится ли ресурс в списке
        /// </summary>
        /// <param name="id">Идентифкатор ресурса</param>
        /// <returns>true - ресурс в списке, false - ресурса нет в списке</returns>
        public bool ContainsResource(int id)
        {
            lock (_resourcesLock)
            {
                return _resources.ContainsKey(id);
            }
        }

        /// <summary>
        /// Удаляет старые ресурсы
        /// </summary>
        public void RemoveOldResources()
        {
            lock (_resourcesLock)
            {
                var oldResources = _resources.Where(p => (DateTime.Now - p.Value.DtCreated).TotalMinutes > RESOURCE_LIFE_TIME).ToList();

                foreach (var resource in oldResources)
                {
                    _resources.Remove(resource.Key);
                }
            }
        }

        /// <summary>
        /// Возвращает часть ресурса
        /// </summary>
        /// <param name="transferPartIdent">
        /// The transfer Part Ident.
        /// </param>
        /// <param name="partSize">
        /// The part Size.
        /// </param>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// Часть ресурса
        /// </returns>
        public TransferPart ReturnPart(TransferPartIdent transferPartIdent, int partSize, Func<IEnumerable> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");
            if (partSize <= 0)
                throw new ArgumentException(@"Размер блока должен быть положительным числом", "partSize");

            TransferPart result;

            if (transferPartIdent == null)
            {
                var items = func();
                var resourceId = this.AddResource(items, partSize);

                result = this.GetPart(resourceId, 0);

                if (result.IsLast)
                    this.RemoveResource(resourceId);

                return result;
            }

            if (!this.ContainsResource(transferPartIdent.ResourceId))
                throw new ArgumentException(string.Format("Ресурс с идентификатором {0} не найден.", transferPartIdent.ResourceId));

            result = this.GetPart(transferPartIdent.ResourceId, transferPartIdent.PartNumber + 1);

            if (result.IsLast)
                this.RemoveResource(transferPartIdent.ResourceId);

            return result;
        }

        /// <summary>
        /// Получает большой объекм данных по частям
        /// </summary>
        /// <typeparam name="T">Тип возвращаемых записей</typeparam>
        /// <param name="func">Ссылка на функцию, получающую часть данных</param>
        /// <returns>Список записей</returns>
        public static List<T> GetByParts<T>(Func<TransferPartIdent, TransferPart> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            var result = new List<T>();

            TransferPartIdent transferPartIdent = null;

            TransferPart currentPart;
            do
            {
                currentPart = func(transferPartIdent);
                transferPartIdent = currentPart.TransferPartIdent;

                var values = currentPart.Items.Cast<T>();

                result.AddRange(values);
            }
            while (!currentPart.IsLast);

            return result;
        }
    }
}
