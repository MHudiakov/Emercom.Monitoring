using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAL.WCF.ServiceReference;
using DAL.WCF;
using DAL.WCF.Enums;
using DevExpress.XtraLayout.Utils;
using Init.Tools.UI;

namespace Client.UI.SpecialForms
{
    /// <summary>
    /// Окно комплектации выбранного объекта
    /// </summary>
    public partial class fmEquipment : XtraForm
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public fmEquipment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Объект
        /// </summary>
        private Unit Unit { get; set; }

        //private List

        /// <summary>
        /// Создание формы списка движения
        /// </summary>
        public static bool Execute(Unit unit)
        {
            using (var fm = new fmEquipment())
            {
                fm.Unit = unit;

                return fm.ShowDialog() == DialogResult.OK;
            }
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void fmEquipment_Load(object sender, EventArgs e)
        {
            lciUniqForStore.Visibility = Unit.IsStore ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciUniqForUnit.Visibility = Unit.IsStore ? LayoutVisibility.Never : LayoutVisibility.Always;
            lciNonUniqForStore.Visibility = Unit.IsStore ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciNonUniqForUnit.Visibility = Unit.IsStore ? LayoutVisibility.Never : LayoutVisibility.Always;

            gcUniqEquipment.DataSource = SearchUniqEquipmentObject(Unit);
            gcNonUniqEquipment.DataSource = SearchNonUniqEquipmentObject(Unit);

            lueEquipment.Properties.DataSource = DalContainer.WcfDataManager.EquipmentList;
            lueUnit.Properties.DataSource = DalContainer.WcfDataManager.UnitList;
            lueUnit.EditValue = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.Id == Unit.Id).Id;

            deFrom.EditValue = DateTime.Today.Date.AddDays(-1);
            deTo.EditValue = DateTime.Today.Date.AddDays(1);
            teFrom.EditValue = new TimeSpan(0, 0, 0);
            teTo.EditValue = new TimeSpan(0, 0, 0);

            gcMovement.DataSource = GetMovementList();
        }

        /// <summary>
        /// Получение списка движения по объекту/оборудованию
        /// </summary>
        /// <returns></returns>
        private List<Movement> GetMovementList()
        {
            var list = DalContainer.WcfDataManager.ServiceOperationClient.GetMovementListByTimeAndUnitId(From, To, Unit.Id);

            if (lueEquipment.EditValue != null)
                list = list.Where(m => m.EquipmentId == (int)lueEquipment.EditValue).ToList();

            list.Reverse();
            return list;
        }

        /// <summary>
        /// Вывод формы движения по двойному клику на уникальном оборудовании
        /// </summary>
        private void gcUniqEquipment_DoubleClick(object sender, EventArgs e)
        {
            var focusedEquipment = gvUniqEquipment.GetFocusedRow() as UniqEquipmentObject;

            tabbedControlGroup.SelectedTabPage = lcgMovement;
            lueEquipment.EditValue = DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == focusedEquipment.EquipmentId).Id;

            gcMovement.DataSource = GetMovementList();
        }

        #region UniqEquipment page

        /// <summary>
        /// Метод заполнения списка уникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Список уникального оборудования объекта</returns>
        private List<UniqEquipmentObject> SearchUniqEquipmentObject(Unit obj)
        {
            var list = new List<UniqEquipmentObject>();
            var store = DalContainer.WcfDataManager.UnitList.FirstOrDefault(u => u.IsStore);
            var helpList = obj.IsStore
                           ? DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId == obj.Id).ToList()
                           : DalContainer.WcfDataManager.UniqEquipmentList.Where(eq => eq.UnitId != store.Id).ToList();

            foreach (var item in helpList)
            {
                var uniqEquipment = DalContainer.WcfDataManager.UniqEquipmentList.FirstOrDefault(ueq => ueq.Id == item.Id);
                var equipment = (uniqEquipment.UnitId == obj.Id || obj.IsStore) ? DalContainer.WcfDataManager.EquipmentList.FirstOrDefault(eq => eq.Id == uniqEquipment.EquipmentId) : uniqEquipment.Equipment;
                if (equipment == null)
                    continue;

                if (obj.IsStore || uniqEquipment.UnitId == obj.Id)
                {
                    uniqEquipment.ColorForAppearance = !equipment.IsArrivedBool ? (int)ColourEnum.Red : (equipment.LastUnitId == uniqEquipment.UnitId ? (int)ColourEnum.Green : (int)ColourEnum.Grey);
                    list.Add(uniqEquipment);
                }
                else
                {
                    if (equipment.LastUnitId == obj.Id && equipment.IsArrivedBool)
                    {
                        uniqEquipment.ColorForAppearance = (int)ColourEnum.Yellow; // yellow
                        list.Add(uniqEquipment);
                    }
                }
            }

            return list;
        }

        #endregion

        #region NonUniqEquipment page

        /// <summary>
        /// Метод заполнения списка неуникального оборудования и определения цвета строк оного
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Список неуникального оборудования объекта</returns>
        private List<NonUniqEquipmentObject> SearchNonUniqEquipmentObject(Unit obj)
        {
            var list = new List<NonUniqEquipmentObject>();
            var nonUniqList = DalContainer.WcfDataManager.NonUniqEquipmentList.Where(eq => eq.UnitId == obj.Id).ToList();
            var allEquipment = DalContainer.WcfDataManager.EquipmentList.Where(eq => !eq.IsUniq).ToList();

            if (!obj.IsStore)
                allEquipment = allEquipment.Where(eq => eq.LastUnitId == obj.Id && eq.IsArrivedBool).ToList();

            foreach (var item in nonUniqList)
            {
                var nonUniqEquipment = DalContainer.WcfDataManager.NonUniqEquipmentList.FirstOrDefault(nueq => nueq.Id == item.Id);
                var listEquipment = allEquipment.Where(eq => eq.kEquipmentId == nonUniqEquipment.kEquipmentId).ToList();

                var isRed = obj.IsStore
                            ? (listEquipment.Any(eq => (eq.LastMovement == null) || (eq.LastMovement != null && !eq.IsArrivedBool)))
                            : listEquipment.Count == 0;
                var isGreen = !isRed && obj.IsStore
                                        ? listEquipment.All(eq => eq.IsInStore)
                                        : listEquipment.Count >= nonUniqEquipment.Count;

                nonUniqEquipment.ColorForAppearance = isRed ? (int)ColourEnum.Red : (isGreen ? (int)ColourEnum.Green : (int)ColourEnum.Grey);
                nonUniqEquipment.RealCount = obj.IsStore ? listEquipment.Count(eq => eq.IsInStore && eq.IsArrivedBool) : listEquipment.Count;

                list.Add(nonUniqEquipment);
            }

            return list;
        }

        #endregion

        /// <summary>
        /// Обновление страницы
        /// </summary>
        private void tmRefresh_Tick(object sender, EventArgs e)
        {
            gcUniqEquipment.DataSource = SearchUniqEquipmentObject(Unit);
            gcNonUniqEquipment.DataSource = SearchNonUniqEquipmentObject(Unit);
        }

        /// <summary>
        /// Дата-время начала
        /// </summary>
        private DateTime From
        {
            get
            {
                var res = this.deFrom.DateTime.Date;
                res = res.Add(teFrom.Time.TimeOfDay);

                return res;
            }
        }

        /// <summary>
        /// Дата-время окончания
        /// </summary>
        private DateTime To
        {
            get
            {
                var res = this.deTo.DateTime.Date;
                res = res.Add(teTo.Time.TimeOfDay);

                return res;
            }
        }

        /// <summary>
        /// Очистка поля оборудования
        /// </summary>
        private void lueEquipmentClick(object sender, EventArgs e)
        {
            lueEquipment.EditValue = null;
        }

        /// <summary>
        /// Загрузка списка движения по фильтрам формы
        /// </summary>
        private void sbShow_Click(object sender, EventArgs e)
        {
            if (From > To)
                xMsg.Information("Дата начала периода наблюдения не может быть похже даты конца!");
            else
                gcMovement.DataSource = GetMovementList();
        }

    }
}