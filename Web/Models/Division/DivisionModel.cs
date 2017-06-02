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
            this.Name = division.ToString();
            this.Description = division.Description;
            this.ParentId = division.ParentId;
        }

        public int Id { get; set; }

        /// <summary>
        /// Название объекта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Описание/примечание объекта
        /// </summary>
        public string Description { get; set; }
    }
}
