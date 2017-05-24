using System;
using Ble.Common.Enums;

namespace Server.Dal.Sql.DataObjects
{
    using System.Runtime.Serialization;

    using Init.DbCore;
    using Init.DbCore.DB.Metadata;
    using Init.DbCore.Metadata;

    /// <summary>
    /// Пользователь
    /// </summary>
    [DataContract]
    [DbTable("User")]
    public sealed class User : DbObject
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
        /// Id подразделения, к которому приписан пользователь
        /// </summary>
        [DataMember]
        [DbMember("DivisionId", typeof(int))]
        public int DivisionId { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [DataMember]
        [DbMember("Login", typeof(string))]
        public string Login { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [DataMember]
        [DbMember("PasswordHash", typeof(string))]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        [DataMember]
        [DbMember("Name", typeof(string))]
        public string Name { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [DataMember]
        [DbMember("Role", typeof(int))]
        public int RoleId { get; set; }

        public UserRole Role
        {
            get
            {
                return (UserRole)Enum.ToObject(typeof(UserRole), RoleId);
            }
            set
            {
                RoleId = (int) value;
            }
        }

        /// <summary>
        /// Описание
        /// </summary>
        [DataMember]
        [DbMember("Description", typeof(string))]
        public string Description { get; set; }
    }
}