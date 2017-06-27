using System;

namespace Web.Models.Division
{
    using DAL.WCF.ServiceReference;
    public class DivisionModel
    {
        public DivisionModel(Division division)
        {
            if (division == null)
                throw new ArgumentNullException(nameof(division));

            this.Id = division.Id;
            this.Name = division.Name;
            this.Description = division.Description;
            this.ParentId = division.ParentId;
        }

        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        public string Description { get; set; }
    }
}
