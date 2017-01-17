// --------------------------------------------------------------------------------------------------------------------
// <copyright file="kMapIcons.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2014г.
// </copyright>
// <summary>
//   Перечисление названий картинок, которые можно использовать в картографии
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Maps
{
    /// <summary>
    /// Перечисление названий картинок, которые можно использовать в картографии
    /// </summary>
    public enum kMapIcons
    {
        /// <summary>
        /// Нет картинки
        /// </summary>
        NoImage,

        /// <summary>
        /// Картинка машины онлайн с координатами
        /// </summary>
        MapCar,

        /// <summary>
        /// Картинка машины в статусе "Свободен"
        /// </summary>
        FreeCar,

        /// <summary>
        /// Картинка машины в статусе "Занят"
        /// </summary>
        BusyCar,

        /// <summary>
        /// Картинка машины оффлайн
        /// </summary>
        MapCarOffline,

        /// <summary>
        /// Картинка машины онлайн с без координат
        /// </summary>
        MapCarWithoutCoordinates,

        /// <summary>
        /// Картинка дома
        /// </summary>
        MapHouse,

        /// <summary>
        /// Картинка улицы
        /// </summary>
        MapStreet,

        /// <summary>
        /// Картинка зоны
        /// </summary>
        MapZone,

        /// <summary>
        /// Картинка превышения скорости
        /// </summary>
        IconSpeedThreshold,

        /// <summary>
        /// Картинка окончания заказа
        /// </summary>
        MapOrderEndPoint,

        /// <summary>
        /// Картинка начала заказа
        /// </summary>
        MapStartPoint,

        /// <summary>
        /// База
        /// </summary>
        Store
    }
}
