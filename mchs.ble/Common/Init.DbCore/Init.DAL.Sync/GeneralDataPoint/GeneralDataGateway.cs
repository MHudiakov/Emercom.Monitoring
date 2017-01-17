// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeneralDataGateway.cs" company="ИНИТ-центр">
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
    using System.ServiceModel;

    using Init.DAL.Sync.Common;
    using Init.DbCore;
    using Init.DbCore.DataAccess;
    using Init.DbCore.Wcf;

    /// <summary>
    /// Шлюз доступа к данным посредством универсльной точки обмена данными
    /// </summary>
    /// <typeparam name="T">
    /// Тип шлюза записи
    /// </typeparam>
    /// <typeparam name="TClientInstanceType">Тип прокси объекта</typeparam>
    public class GeneralDataGateway<T, TClientInstanceType> : DataAccess<T, WcfContextBase<TClientInstanceType>>
        where T : DbObject
        where TClientInstanceType : class, IGeneralDataPoint, ICommunicationObject, new()
    {
        /// <summary>
        /// Тип шлюза записи
        /// </summary>
        private readonly string _typeName;

        /// <summary>
        /// Инициализирует новый экземпляр класса универсального шлюза доступа к данным
        /// Шлюз доступа к данным посредством универсльной точки обмена данными
        /// </summary>
        /// <param name="getContext">
        /// Метод получения ссылки на контекст доступа к данным
        /// </param>
        public GeneralDataGateway(Func<WcfContextBase<TClientInstanceType>> getContext)
            : base(getContext)
        {
            _typeName = typeof(T).Name;
        }

        /// <summary>
        /// Размер части при выгрузке списка.
        /// </summary>
        protected int ConstListSlice
        {
            get
            {
                return 100;
            }
        }

        /// <summary>
        /// Получение всех объектов
        /// </summary>
        /// <returns>
        /// Список всех объектов в таблице
        /// </returns>
        public override List<T> GetAll()
        {
            return Transfer.TransferManager.GetByParts<T>(ident => this.Context.CommunicationObject.TransferItems(this._typeName, ident));
        }

        /// <summary>
        /// Получение количества объектов
        /// </summary>
        /// <returns>
        /// Количество объектов в БД
        /// </returns>
        public override long GetCount()
        {
            return this.Context.CommunicationObject.Count(this._typeName);
        }

        /// <summary>
        /// Добавление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void AddOverride(T item)
        {
            var change = Context.CommunicationObject.Add(new Change(ChangeTypeEnum.Add, _typeName, DateTime.Now, new Dictionary<string, object>(), item));
            if (change != null)
            {
                var changedItem = change.GetItem<T>();
                changedItem.CopyTo(item);
            }
        }

        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="item">Объект</param>
        protected override void EditOverride(T item)
        {
            Context.CommunicationObject.Edit(
                new Change(
                    ChangeTypeEnum.Edit,
                    _typeName,
                    DateTime.Now,
                    new Dictionary<string, object> { { item.Key.PropertyInfo.Name, item.KeyValue } },
                    item));
        }

        /// <summary>
        /// Выполнят удаление записей по совпдению указанных полей
        /// </summary>
        /// <param name="whereArgs">
        /// Ключи объекта
        /// </param>
        protected override void DeleteWhereOverride(Dictionary<string, object> whereArgs)
        {
            Context.CommunicationObject.DeleteWhere(new Change(ChangeTypeEnum.Delete, _typeName, DateTime.Now, whereArgs, null));
        }

        /// <summary>
        /// Получение сиска объектов по набору ключей ключу
        /// </summary>
        /// <param name="whereArgs">
        /// Ключ выбора объектов
        /// </param>
        /// <returns>
        /// Список объектов подходящий под критерий
        /// </returns>
        protected override List<T> GetItemsWhereOverride(Dictionary<string, object> whereArgs)
        {
            var res = Context.CommunicationObject.GetItemsWhere(_typeName, whereArgs);
            return res.Select(e => e.GetItem<T>()).ToList();
        }

        /// <summary>
        /// Получение части объектов
        /// </summary>
        /// <param name="pageIndex">
        /// Номер страницы
        /// </param>
        /// <param name="pageSize">
        /// Количество объектов
        /// </param>
        /// <returns>
        /// Список объектов со страницы pageIndex
        /// </returns>
        protected override List<T> GetPageOverride(int pageIndex, int pageSize)
        {
            return Context.CommunicationObject.GetPage(_typeName, pageIndex, pageSize).Select(c => c.GetItem<T>()).ToList();
        }
    }
}