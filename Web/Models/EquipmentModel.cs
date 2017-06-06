namespace Web.Models
{
    using System;
    using DAL.WCF.ServiceReference;

    /// <summary>
    /// Модель оборудования
    /// </summary>
    public class EquipmentModel
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="EquipmentModel"/>.
        /// </summary>
        /// <param name="equipment">
        /// The equipment.
        /// </param>
        public EquipmentModel(Equipment equipment)
        {
            if (equipment == null)
                throw new ArgumentNullException(nameof(equipment));

            this.Id = equipment.Id;
            this.Tag = equipment.Tag;
            this.KEquipmentId = equipment.KEquipmentId;
            this.Description = equipment.Description;
        }

        #region DataFields
        public int Id { get; set; }

        public string Tag { get; set; }

        public int KEquipmentId { get; set; }

        public string Description { get; set; }

        #endregion
    }
}