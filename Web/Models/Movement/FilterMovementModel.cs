using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    using System;
    using System.Web.Mvc;
    using DAL.WCF;
    
    public class FilterMovementModel
    {
        public FilterMovementModel(int unitId)
        {
            var equipmentList = DalContainer.WcfDataManager.ServiceOperationClient.GetEquipmentListForUnit(unitId);
            this.EquipmentList = new SelectList(equipmentList, "Id", "Tag");
            this.UnitId = unitId;
        }

        public FilterMovementModel()
        {
        }
        
        [Required(ErrorMessage = "Укажите значение поля")]
        [DataType(DataType.DateTime, ErrorMessage = "Starange error")]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DtBegin { get; set; }
        
        [Required(ErrorMessage = "Укажите значение поля")]
        [DataType(DataType.DateTime, ErrorMessage = "Starange error")]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? DtEnd { get; set; }
        
        public int? EquipmentId { get; set; }
        
        public int UnitId { get; set; }
        
        public SelectList EquipmentList { get; set; }
    }
}