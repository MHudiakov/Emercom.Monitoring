using System;
using Ble.Common.Enums;

namespace Web.Models.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using DAL.WCF;
    using DAL.WCF.ServiceReference;

    public class UserModel
    {
        public UserModel(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            this.Id = user.Id;
            this.Login = user.Login;
            this.Name = user.Name;
            this.IsAdmin = user.Role == UserRole.Administrator;
            this.DivisionName = user.GetDivision.ToString();
            this.Description = user.Description;
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public int RoleId { get; set; }

        public int DivisionId { get; set; }

        public string DivisionName { get; set; }

        public string Description { get; set; }

        public SelectList DivisionList
        {
            get
            {
                var dividionList = DalContainer.WcfDataManager.DivisionList;
                return new SelectList(dividionList.OrderBy(e => e.Name), "Id", "Name");
            }
        }

        public IEnumerable<SelectListItem> UserRoleList
        {
            get
            {
                var enumValues = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().ToList();
                if (enumValues == null)
                    return null;

                return enumValues.Select(enumValue => 
                new SelectListItem { Value = enumValue.ToString(),
                    Text = enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().Name }).ToList();
            }
        }
    }
}