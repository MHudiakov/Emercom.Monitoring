namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Настройки сервера
    /// </summary>
    [DataContract]
    [DbTable("Settings")]
    public sealed class Settings : DbObject
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
        /// Адрес WFC сервиса
        /// </summary>
        [DataMember]
        [DbMember("WcfServiceAddress", typeof(string))]
        public string WcfServiceAddress { get; set; }
    }
}