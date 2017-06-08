namespace Web.Models
{
    using System;
    using DAL.WCF.ServiceReference;

    public class MovementModel
    {
        public MovementModel(Movement movement)
        {
            if (movement == null)
                throw new ArgumentNullException(nameof(movement));

           this.Id = movement.Id;
           this.UnitId = movement.UnitId;
           this.EquipmentId = movement.EquipmentId;
           this.IsArrived = movement.IsArrived;
           this.Date = movement.Date;
           this.Equipment = movement.Equipment;
        }

        public Equipment Equipment { get; set; }

        public int UnitId { get; set; }

        public int Id { get; set; }

        public int? EquipmentId { get; set; }

        public bool IsArrived { get; set; }

        public DateTime Date { get; set; }

    }
}