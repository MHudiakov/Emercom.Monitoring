using System;
using System.Linq;
using Ble.Common.Enums;
using Microsoft.AspNet.Identity;

namespace DAL.WCF.ServiceReference
{
    public partial class User : IUser<int>
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

        public string UserName {
            get { return Name; }
            set { Name = value; }
        }
    }
}