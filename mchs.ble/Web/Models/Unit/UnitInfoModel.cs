using System.Linq;

namespace Web.Models.Unit
{
    using System.Collections.Generic;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UnitInfoModel
    {
        // сделать отдельными моделями
        public UnitModel Unit { get; private set; }

        public List<MovementModel> MovmentList { get; private set; }

        public UnitInfoModel(Unit unit)
        {
            this.Unit = new UnitModel(unit);
            
       /*     if (this.Unit.IsStore)
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
                    
                    if (model.UnitId == this.Unit.Id)
                    {
                        model.ColorForAppearance = model.IsArrived ? model.LastUnitId == model.UnitId ? 1 : 2 : 3;
                        this.UniqEquipmentObjectList.Add(model);
                    }
                    else if (model.LastUnitId == this.Unit.Id && model.IsArrived)
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
                if (this.Unit.IsStore)
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
            this.MovmentList = DalContainer.WcfDataManager.MovementList
                .Where(m => m.UnitId == unit.Id).OrderByDescending(m => m.DateOfMovement)
                .Take(50).Select(e => new MovementModel(e)).ToList();*/
        }
    }
}