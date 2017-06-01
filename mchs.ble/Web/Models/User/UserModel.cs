using System;
using Ble.Common.Enums;

namespace Web.Models.User
{
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
            this.DivisionName = user.GetDivision.Name;
            this.Description = user.Description;
        }

        public int Id { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }

        public string DivisionName { get; set; }

        public string Description { get; set; }
    }
}