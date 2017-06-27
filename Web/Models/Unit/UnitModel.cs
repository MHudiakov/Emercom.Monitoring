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
            this.DivisionName = unit.GetDivision.ToString();
            this.Name = unit.Name;
            this.Number = unit.Number;
            this.Description = unit.Description;
        }

        public int Id { get; set; }
        
        public string DivisionName { get; set; }
        
        public string Name { get; set; }
        
        public string Number { get; set; }
        
        public string Description { get; set; }
    }
}