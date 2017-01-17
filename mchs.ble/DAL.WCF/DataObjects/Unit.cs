namespace DAL.WCF.ServiceReference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Объект
    /// </summary>
    public partial class Unit
    {

        private kObject _kobj = null;

        /// <summary>
        /// Тип оборудования
        /// </summary>
        public kObject kObject
        {
            get
            {
                if (_kobj == null)
                    _kobj = DalContainer.WcfDataManager.kObjectList.SingleOrDefault(e => e.Id == this.kObjectId);

                return _kobj;
            }
        }

        /// <summary>
        /// Название типа объекта
        /// </summary>
        public string TypeName { get { return this.kObject != null ? this.kObject.Type : ""; } }

        /// <summary>
        /// Является ли объект складом
        /// </summary>
        public bool IsStore { get { return this.kObject.IsStore; } }

        public int Type { get { return this.IsStore ? 1 : 2; } }

        public string BortNumName
        {
            get
            {
                var bortnum = this.BortNum ?? "";
                return bortnum + "/" + this.Name;
            }
        }

        /// <summary>
        /// Задаётся номер, которому соответствует цвет раскраски таблицы
        /// </summary>
        public int ColorForAppearance
        {
            get;
            // {
            //     var uniqList = SearchUniqEquipmentObject(this);
            //     var nonUniqList = SearchNonUniqEquipmentObject(this);
            //     var indexlist = new List<int>();
            // 
            //     foreach (var item in uniqList)
            //         indexlist.Add(item.ColorForAppearance);
            //     foreach (var item in nonUniqList)
            //         indexlist.Add(item.ColorForAppearance);
            // 
            //     return indexlist.Exists(n => n == 3) ? 3 : (indexlist.Exists(n => n == 2) ? 2 : (indexlist.Exists(n => n == 4) ? 4 : 1));
            // }

            set;
        }

    }
}