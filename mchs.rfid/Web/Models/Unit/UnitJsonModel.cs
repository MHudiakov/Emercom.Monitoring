using System.Collections.Generic;
using System.Linq;

namespace Web.Models.Unit
{
    using System;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UnitJsonModel
    {
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

        public List<UniqEquipmentObjectModel> UniqEquipmentObjectList { get; set; }

        public List<NonUniqEquipmentObjectModel> NonUniqEquipmentObjectList { get; private set; }

        public List<MovementModel> MovmentList { get; private set; }

        public UnitJsonModel(Unit unit)
        {
            this.Id = unit.Id;
            this.Name = unit.Name;
            this.Description = unit.Description;
            this.kObjectId = unit.kObjectId;
            this.Type = unit.TypeName;
            this.IsStore = unit.IsStore;

            this.UniqEquipmentObjectList = new List<UniqEquipmentObjectModel>();
            if (this.IsStore)
            {
                foreach (var unEquip in DalContainer.WcfDataManager.UniqEquipmentList.Where(e => e.UnitId == unit.Id))
                {
                    var model = new UniqEquipmentObjectModel(unEquip);
                    model.ColorForAppearance = model.IsArrived ? model.LastUnitId == model.UnitId ? 1 : 2 : 3;
                    this.UniqEquipmentObjectList.Add(model);
                }
            }
            else
            {
                var store = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.IsStore);
                foreach (var unEquip in DalContainer.WcfDataManager.UniqEquipmentList.Where(e => e.UnitId != store.Id))
                {
                    var model = new UniqEquipmentObjectModel(unEquip);
                    
                    if (model.UnitId == this.Id)
                    {
                        model.ColorForAppearance = model.IsArrived ? model.LastUnitId == model.UnitId ? 1 : 2 : 3;
                        this.UniqEquipmentObjectList.Add(model);
                    }
                    else if (model.LastUnitId == this.Id && model.IsArrived)
                    {
                        model.ColorForAppearance = 4;
                        this.UniqEquipmentObjectList.Add(model);
                    }
                }
            }

            this.NonUniqEquipmentObjectList = new List<NonUniqEquipmentObjectModel>();
            foreach (var nonunEquip in DalContainer.WcfDataManager.NonUniqEquipmentList.Where(e => e.UnitId == unit.Id))
            {
                var model = new NonUniqEquipmentObjectModel(nonunEquip);
                var list = DalContainer.WcfDataManager.EquipmentList
                        .Where(e => e.kEquipmentId == model.kEquipmentId).ToList();
                model.RealCount = list.Count;
                if (this.IsStore)
                {
                    var isRed = list.Any(e => e.LastMovement != null && !e.IsArrivedBool);
                    var isInStore = list.All(e => e.IsInStore);
                    if (isInStore && !isRed)
                        model.ColorForAppearance = 1;
                    if (!isInStore && !isRed)
                        model.ColorForAppearance = 2;
                    if (isRed)
                        model.ColorForAppearance = 3;
                }
                else
                {
                    if (model.RealCount >= model.Count)
                        model.ColorForAppearance = 1;
                    else if (model.RealCount < model.Count && model.RealCount > 0)
                        model.ColorForAppearance = 2;
                    else if (model.RealCount == 0)
                        model.ColorForAppearance = 3;
                }
                this.NonUniqEquipmentObjectList.Add(model);
            }

            // последние 50 движений которые отображаем
            this.MovmentList = DalContainer.WcfDataManager.ServiceOperationClient
                .GetMovementListByTimeAndUnitId(DateTime.MinValue, DateTime.Now, unit.Id).OrderByDescending(m => m.DateOfMovement)
                .Take(50).Select(e => new MovementModel(e)).ToList();
        }
    }
}