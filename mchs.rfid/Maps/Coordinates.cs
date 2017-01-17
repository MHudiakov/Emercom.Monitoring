// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Coordinates.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Представляет географическую координату точки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Представляет географическую координату точки
    /// </summary>
    [DataContract]
    public class Coordinates
    {
        /// <summary>
        /// Долгота точки
        /// </summary>
        [DataMember]
        public double Longitude { get; set; }

        /// <summary>
        /// Широта точки
        /// </summary>
        [DataMember]
        public double Latitude { get; set; }
    }
}