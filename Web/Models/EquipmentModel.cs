using System.Web.WebPages;

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
            this.Name = equipment.Name.IsEmpty() ? equipment.KEquipment.Name : equipment.Name;
            this.Tag = equipment.Tag;
            this.KEquipmentId = equipment.KEquipmentId;
            this.Description = equipment.Description;
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Tag { get; set; }

        public int KEquipmentId { get; set; }

        public string Description { get; set; }
    }
}