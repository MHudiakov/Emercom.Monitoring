// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectA.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Класс А-заглушка
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Init.DAL.Sync.Test.DataObjects
{
    using Init.DAL.Sync.Test.Mocks;
    using Init.DbCore;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Класс А-заглушка
    /// </summary>
    [DataContract]
    public class ObjectA : DbObject
    {
        /// <summary>
        /// Кеш объектов B
        /// </summary>
        private List<ObjectB> _objectBList;

        /// <summary>
        /// Список объектов Б
        /// </summary>
        public List<ObjectB> ObjectBList
        {
            get
            {
                if (_objectBList == null)
                    _objectBList = Container.DataManager.ObjectsB.GetAll().Where(e => e.ObjectAId == Id).ToList();
                return _objectBList;
            }
        }

        /// <summary>
        /// Ключ
        /// </summary>
        [DbIdentity]
        [DbKey]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Наименование объекта
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}
