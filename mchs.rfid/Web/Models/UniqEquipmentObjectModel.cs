namespace Web.Models
{
    using DAL.WCF.ServiceReference;

    public class UniqEquipmentObjectModel
    {
        public UniqEquipmentObjectModel(UniqEquipmentObject item)
        {
            this.Id = item.Id;
            this.UnitId = item.UnitId;
            this.EquipmentId = item.EquipmentId;
            this.EquipmentName = item.EquipmentName;
            this.EquipmentGroup = item.EquipmentGroup;
            this.EquipmentDescription = item.EquipmentDescription;
            this.ColorForAppearance = item.ColorForAppearance;
            this.IsArrived = item.Equipment != null && item.Equipment.IsArrivedBool;
            this.LastUnitId = item.Equipment != null ? item.Equipment.LastUnitId : 0;
        }

        public int Id { get; set; }

        /// <summary>
        /// Id объекта
        /// </summary>
        public int UnitId { get; set; }

        /// <summary>
        /// Id оборудования
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// Имя оборудования
        /// </summary>
        public string EquipmentName { get; private set; }

        /// <summary>
        /// Группа оборудования
        /// </summary>
        public string EquipmentGroup { get; private set; }

        /// <summary>
        /// Описание оборудования
        /// </summary>
        public string EquipmentDescription { get; private set; }

        /// <summary>
        /// Задаётся номер, которому соответствует цвет раскраски таблицы
        /// </summary>
        public int ColorForAppearance { get; set; }

        public bool IsArrived { get; set; }

        public int LastUnitId { get; set; }
    }
}