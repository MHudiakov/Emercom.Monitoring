using System;
using System.Linq;
using Ble.Common.Enums;

namespace DAL.WCF.ServiceReference
{
    public partial class User
    {
        public Division GetDivision => DalContainer.WcfDataManager.DivisionList.Single(d => d.Id == DivisionId);

        public UserRole Role
        {
            get
            {
                return (UserRole)Enum.ToObject(typeof(UserRole), RoleId);
            }
            set
            {
                RoleId = (int)value;
            }
        }
    }
}