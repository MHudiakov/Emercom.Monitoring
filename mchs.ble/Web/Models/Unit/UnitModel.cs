namespace Web.Models.Unit
{
    using DAL.WCF.ServiceReference;

    public class UnitModel
    {
        public UnitModel(Unit unit)
        {
            this.Id = unit.Id;
            this.DivisionId = unit.DivisionId;
            this.Name = unit.Name;
            this.Number = unit.Number;
            this.Description = unit.Description;
        }

        public int Id { get; set; }

        /// <summary>
        /// Ид подразделения, которому принадлежит юнит
        /// </summary>
        public int DivisionId { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер объекта
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }
    }
}