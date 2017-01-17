namespace Web.Models
{
    using DAL.WCF.ServiceReference;

    public class NonUniqEquipmentObjectModel
    {
        public NonUniqEquipmentObjectModel(NonUniqEquipmentObject item)
        {
            this.Id = item.Id;
            this.UnitId = item.UnitId;
            this.kEquipmentId = item.kEquipmentId;
            this.Count = item.Count;
            this.RealCount = item.RealCount;
            this.kEquipmentName = item.kEquipmentName;
            this.kEquipmentGroup = item.kEquipmentGroup;
            this.kEquipmentDescription = item.kEquipmentDescription;
            this.ColorForAppearance = item.ColorForAppearance;
        }

        public int Id { get; set; }

        /// <summary>
        /// Id объекта
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// Id типа оборудования
        /// </summary>
        public int kEquipmentId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Кол-во оборудования в объекте
        /// </summary>
        public int RealCount { get; set; }

        /// <summary>
        /// Имя оборудования
        /// </summary>
        public string kEquipmentName { get; set; }

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public string kEquipmentGroup { get; set; }

        /// <summary>
        /// Описание оборудования
        /// </summary>
        public string kEquipmentDescription { get; set; }

        /// <summary>
        /// Задаётся номер, которому соответствует цвет раскраски таблицы
        /// </summary>
        public int ColorForAppearance { get; set; }

        public bool IsArrived { get; set; }
    }
}