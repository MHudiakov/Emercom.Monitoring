namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Модель машины
    /// </summary>
    [DataContract]
    [DbTable("Car")]
    public class Car : DbObject
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember]
        [DbMember("Id", typeof(int))]
        [DbIdentity]
        [DbKey]
        public int Id { get; set; }

        /// <summary>
        /// Id подразделения, к которому приписана машина
        /// </summary>
        [DataMember]
        [DbMember("DivisionId", typeof(int))]
        public int DivisionId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Номер машины
        /// </summary>
        [DataMember]
        [DbMember("Number", typeof(string))]
        public string Number { get; set; }

        /// <summary>
        /// Описание/примечание группы
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }
    }
}