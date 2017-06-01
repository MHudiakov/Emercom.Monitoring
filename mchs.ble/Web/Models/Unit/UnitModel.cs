using System;

namespace Web.Models.Unit
{
    using DAL.WCF.ServiceReference;

    public class UnitModel
    {
        public UnitModel(Unit unit)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            this.Id = unit.Id;
            this.DivisionName = unit.GetDivision.Name;
            this.Name = unit.Name;
            this.Number = unit.Number;
            this.Description = unit.Description;
        }

        public int Id { get; set; }

        /// <summary>
        /// Название подразделения, которому принадлежит юнит
        /// </summary>
        public string DivisionName { get; set; }

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