﻿using System.Linq;
using System.Web.Mvc;
using DAL.WCF;
using Web.Models.User;

namespace Web.Controllers
{
    using System;

    using Ble.Common;
    using Ble.Common.Enums;

    using DAL.WCF.ServiceReference;

    /// <summary>
    /// Контроллер пользователей
    /// </summary>
    [Authorize(Roles = UserRolesConsts.Administrator)]
    public class UserController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var filter = new FilterUserModel();
            return View(filter);
        }


        [HttpPost]
        public PartialViewResult AddOrEditUser(UserModel userModel)
        {
            var user = new User();
            user.Login = userModel.Login;
            user.RoleId = userModel.RoleId;
            user.PasswordHash = CryptographyHelper.GetSha256Hash(userModel.Password);
            user.Name = userModel.Name;
            user.DivisionId = userModel.DivisionId;
            user.Description = userModel.Description;


            var repository = DalContainer.WcfDataManager.ServiceOperationClient;
            if (userModel.Id != 0)
                repository.EditUser(user);
            else
                repository.AddUser(user);

            var userList = DalContainer.WcfDataManager.UserList;
            var userModelList = userList.Select(userItem => new UserModel(userItem)).OrderBy(userItem => userItem.IsAdmin).ThenBy(userItem => userItem.Name).ToList();
            return PartialView("List", userModelList);
        }

        public PartialViewResult EditUser(int id)
        {
            var user = id == 0 ? new User() : DalContainer.WcfDataManager.UserList.FirstOrDefault(item => item.Id == id);
            var userModel = new UserModel(user);
            return this.PartialView("Edit"userModel);
        }

        public PartialViewResult List(FilterUserModel filter)
        {
            var userList = filter.DivisionId == null ?
                DalContainer.WcfDataManager.UserList :
                DalContainer.WcfDataManager.UserList.Where(user => user.DivisionId == filter.DivisionId);

            var userModelList = userList.Select(user => new UserModel(user)).OrderBy(user => user.IsAdmin).ThenBy(user => user.Name).ToList();
            return PartialView(userModelList);
        }
    }
}