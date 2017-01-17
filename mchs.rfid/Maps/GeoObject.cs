// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GeoObject.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps
{
    using DAL.WCF.Enums;
    using System.Runtime.Serialization;

    /// <summary>
    /// Данные по географическому объекту
    /// </summary>
    [DataContract]
    public class GeoObject
    {
        // Координаты

        /// <summary>
        /// Широта
        /// </summary>
        [DataMember]
        public double Lat { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        [DataMember]
        public double Lon { get; set; }

        /// <summary>
        /// Название адреса
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Тип объекта (текстовый)
        /// </summary>
        [DataMember]
        public UnitTypeEnum Kind { get; set; }

        /// <summary>
        /// Некая точность в виде текста. Enum потом если нужно созадите сами. 
        /// </summary>
        [DataMember]
        public string PrecisionStr { get; set; }
    }
}
