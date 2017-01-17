// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Unit.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Объект
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Server.Dal.SQL.DataObjects
{
    using System.Runtime.Serialization;

    using DAL;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Объект
    /// </summary>
    [DataContract]
    [DbTable("Unit")]
    public class Unit : DbObject
    {
        /// <summary>
        /// Id 
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentityAttribute]
        [DbKeyAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Id класса объекта, к которому относится данный
        /// </summary>
        [DataMember]
        [DbMember("kObjectId", typeof(int))]
        public int kObjectId { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }

        /// <summary>
        /// Серийный номер (Id считывателя)
        /// </summary>
        [DataMember]
        [DbMember("SerialNum", typeof(string))]
        public string SerialNum { get; set; }

        /// <summary>
        /// Гос.номер машины (объекта)
        /// </summary>
        [DataMember]
        [DbMember("GosNum", typeof(string))]
        public string GosNum { get; set; }

        /// <summary>
        /// Бортовой номер машины (объекта)
        /// </summary>
        [DataMember]
        [DbMember("BortNum", typeof(string))]
        public string BortNum { get; set; }

        /// <summary>
        /// Классификатор объекта
        /// </summary>
        public kObject kObject
        {
            get
            {
                return DalContainer.DataManager.kObjectRepository.Get(this.kObjectId);
            }
        }

        /// <summary>
        /// Является ли объект складом
        /// </summary>
        public bool IsStore { get { return this.kObject.IsStore; } }

    }
}
