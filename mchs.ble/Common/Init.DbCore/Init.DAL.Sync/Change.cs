// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Change.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// // <summary>
//   Перечень разрешений для потоков
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.DAL.Sync
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Init.DAL.Sync.Common;

    /// <summary>
    /// Объект к которому был применён один из типов изменения 
    /// </summary>
    [DataContract]
    public class Change
    {
        #region ChageType

        /// <summary>
        /// Идентификатор типа изменения
        /// </summary>
        [DataMember]
        public int ChangeTypeId { get; private set; }

        /// <summary>
        /// Тип изменения
        /// </summary>
        public ChangeTypeEnum ChangeType
        {
            get
            {
                return (ChangeTypeEnum)ChangeTypeId;
            }

            private set
            {
                ChangeTypeId = (int)value;
            }
        }

        #endregion

        #region Item

        /// <summary>
        /// Массив байт обекта
        /// </summary>
        [DataMember]
        public byte[] RawItemData { get; private set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        [DataMember]
        public string TypeName { get; private set; }

        /// <summary>
        /// Объект
        /// </summary>
        private object _item;

        /// <summary>
        /// Получить объект
        /// </summary>
        /// <typeparam name="T">
        /// Тип изменяемого объекта
        /// </typeparam>
        /// <returns>
        /// Изменяемый объект
        /// </returns>
        public T GetItem<T>() where T : class
        {
            if (_item == null)
                _item = this.DeserialiseObject<T>(RawItemData);

            return (T)_item;
        }

        #endregion

        #region Key

        /// <summary>
        /// Массив байт ключа объекта
        /// </summary>
        [DataMember]
        public byte[] RawItemKeyData { get; private set; }

        /// <summary>
        /// Заначение ключа объекта
        /// </summary>
        private Dictionary<string, object> _filterProps;

        /// <summary>
        /// Получить ключ объекта
        /// </summary>
        /// <returns>
        /// Ключи объекта
        /// </returns>
        public Dictionary<string, object> GetFilterProps()
        {
            if (this._filterProps == null)
                _filterProps = DeserialiseObject<Dictionary<string, object>>(RawItemKeyData);

            return this._filterProps;
        }

        /// <summary>
        /// Выполняет сравнение ключей текущего объекта с ключами другого объекта
        /// </summary>
        /// <param name="targetKeys">
        /// Наборк ключей другого объекта
        /// </param>
        /// <returns>
        /// True, если последовательности ключей в точности совпадают
        /// </returns>
        public bool KeyEquals(Dictionary<string, object> targetKeys)
        {
            if (targetKeys == null)
                throw new ArgumentNullException("targetKeys");

            return this.GetFilterProps().SequenceEqual(targetKeys.OrderBy(e => e.Key));
        }

        #endregion

        /// <summary>
        /// Идентификатор изменения
        /// </summary>
        [DataMember]
        public Guid Id { get; private set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        [DataMember]
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Change"/>. 
        /// Создать запись об изменении в обекте
        /// </summary>
        /// <param name="type">
        /// Тип применённого изменения
        /// </param>
        /// <param name="typeName">
        /// Тип объёкта к которому применялось изменение
        /// </param>
        /// <param name="dateTime">
        /// Дата изменения
        /// </param>
        /// <param name="filterProps">
        /// Ключ изменения
        /// </param>
        /// <param name="objectItem">
        /// Измененный объект
        /// </param>
        public Change(ChangeTypeEnum type, string typeName, DateTime dateTime, Dictionary<string, object> filterProps, object objectItem)
        {
            if (type != ChangeTypeEnum.Delete && objectItem == null)
                throw new ArgumentNullException("objectItem");

            if (filterProps == null)
                throw new ArgumentNullException("filterProps");

            ChangeType = type;
            TypeName = typeName;
            DateTime = dateTime;

            if (objectItem != null)
                RawItemData = SerrializeObject(objectItem);

            this._filterProps = filterProps;
            RawItemKeyData = SerrializeObject(filterProps);

            // Генерируем идентификатор изменения
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Серриализлвать объект в массив байт
        /// </summary>
        /// <param name="item">
        /// Серриализуемый объект
        /// </param>
        /// <returns>
        /// Массив байт
        /// </returns>
        private byte[] SerrializeObject(object item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var ms = new MemoryStream();
            var ser = new DataContractJsonSerializer(item.GetType());
            ser.WriteObject(ms, item);
            ms.Seek(0, SeekOrigin.Begin);
            return ms.ToArray();
        }

        /// <summary>
        /// Выполняет десериализацию объекта
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="data">Дданные объекта</param>
        /// <returns>Десериализованный объект типа T</returns>
        private T DeserialiseObject<T>(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(ms);
            }
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("ChangeType: {0}, TypeName: {1}", ChangeType, TypeName);
        }
    }
}