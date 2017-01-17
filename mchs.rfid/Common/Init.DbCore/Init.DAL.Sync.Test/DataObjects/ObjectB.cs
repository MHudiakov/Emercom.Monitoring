using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Init.DAL.Sync.Test.DataObjects
{
    using Init.DAL.Sync.Test.Mocks;
    using Init.DbCore;
    using Init.DbCore.Metadata;

    [DataContract]
    public class ObjectB : DbObject
    {
        private ObjectA _objectA;
        /// <summary>
        /// список объектов А
        /// </summary>
        public ObjectA ObjectA 
        { 
            get
            {
                if (_objectA != null && _objectA.Id != ObjectAId)
                    _objectA = null;

                if (_objectA == null)
                    _objectA = Container.DataManager.ObjectsA.Get(ObjectAId);

                return _objectA;
            }
            set
            {
                if (value == null)
                    ObjectAId = 0;
                else
                    ObjectAId = value.Id;
                _objectA = value;
            }
        }


        /// <summary>
        /// Id объекта А
        /// </summary>
        [DataMember]
        public int ObjectAId { get; set; }

        /// <summary>
        /// Ключ
        /// </summary>
        [DbIdentity]
        [DbKey]
        [DataMember]
        public int Id { get; set; }
    }
}
