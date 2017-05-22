namespace Web.Models.Unit
{
    using DAL.WCF.ServiceReference;

    public class UnitModel
    {
        public UnitModel(Unit unit)
        {
            this.Id = unit.Id;
          /*  this.Name = unit.Name;
            this.Description = unit.Description;
            this.kObjectId = unit.kObjectId;
            this.Type = unit.TypeName;
            this.IsStore = unit.IsStore;*/
        }

        public int Id { get; set; }

        /// <summary>
        /// Id класса объекта, к которому относится данный
        /// </summary>
        public int kObjectId { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Название типа объекта
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Является ли объект складом
        /// </summary>
        public bool IsStore { get; private set; }
    }
}