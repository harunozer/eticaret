using DataLayer.Models;
using DataLayer.Services;
using HelperLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using wwwAdmin.Models;
using wwwAdmin.Models.FormModel;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class UserController : BaseController
    {
        readonly UserService userService;
        readonly UserRoleService userRoleService;
        readonly CancelService cancelService;

        public UserController(UserService userService_, UserRoleService userRoleService_, CancelService cancelService_)
        {
            userService = userService_;
            userRoleService = userRoleService_;
            cancelService = cancelService_;
        }

        [HttpGet]
        public IActionResult Index()
        {
            UserListModel model = new UserListModel();
            model.InsertPermission = userService.CheckInsertPermission();

            ListFilterItem itemID = new ListFilterItem
            {
                ColName = "ID",
                LabelName = "ID",
                FilterDataType = ListFilterDataType.Numeric,
                FilterType = ListFilterType.Between
            };
            model.ListProps.Filters.Add(itemID);

            ListFilterItem itemName = new ListFilterItem
            {
                ColName = "Name",
                LabelName = "Ad",
                FilterType = ListFilterType.StartsWith
            };
            model.ListProps.Filters.Add(itemName);

            ListFilterItem itemRole = new ListFilterItem
            {
                ColName = "UserRoleID",
                LabelName = "Rol",
                FilterDataType = ListFilterDataType.Numeric,
                FilterInputType = ListFilterInputType.ListBox,
                FilterType = ListFilterType.Equal
            };
            itemRole.InputValues.AddRange(
                userRoleService.getList().Select(i => new InputListItem { Text = i.RoleName, Value = i.ID.ToString() })
            );
            model.ListProps.Filters.Add(itemRole);

            ListFilterItem itemOnline = new ListFilterItem
            {
                ColName = "IsLogin",
                LabelName = "Online",
                FilterDataType = ListFilterDataType.Bool,
                FilterInputType = ListFilterInputType.Radio,
                FilterType = ListFilterType.Equal,
            };
            itemOnline.BindDefaultBoolInputValues();
            model.ListProps.Filters.Add(itemOnline);

            ListFilterItem itemCreateTime = new ListFilterItem
            {
                ColName = "CreateTime",
                LabelName = "Kayıt Tarihi",
                FilterDataType = ListFilterDataType.Date,
                FilterType = ListFilterType.Between
            };
            model.ListProps.Filters.Add(itemCreateTime);

            model.BuildQuery(HttpContext, userService.getListQuery());
            userService.BindPermissionsList(model.DataList);

            base.Model_ = model;
            return View(model);
        }

        [HttpGet]
        public IActionResult Form(int? id)
        {
            //UserAdd - UserUpdate Form
            UserFormModel model = new UserFormModel();
            model.FormType = id != null ? FormType.Update : FormType.Insert;

            if (model.FormType == FormType.Insert)
            {
                if (!userService.CheckInsertPermission())
                    throw new PermissionException();

            }
            else if (model.FormType == FormType.Update)
            {
                model.Data = userService.Get((int)id);
                if (!model.Data.Permissions.Update)
                    throw new PermissionException();
            }

            BindFormModel(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Form(UserFormModel model)
        {
            if (!ModelState.IsValid)
            {
                BindFormModel(model);
                return View(model);
            }

            model.FormType = model.Data.ID == 0 ? FormType.Insert : FormType.Update;
            try
            {
                if (model.FormType == FormType.Insert)
                {
                    userService.Add(model.Data);
                }
                else if (model.FormType == FormType.Update)
                {
                    User updatedData = userService.Get(model.Data.ID);
                    updatedData.Name = model.Data.Name;
                    updatedData.Surname = model.Data.Surname;
                    updatedData.EMail = model.Data.EMail;
                    updatedData.Gsm = model.Data.Gsm;
                    updatedData.Password = model.Data.Password;
                    updatedData.UserRoleID = model.Data.UserRoleID;
                    updatedData.CancelID = model.Data.CancelID;

                    userService.Update(updatedData);
                }
            }
            catch (ValidationException exValidation)
            {
                setErrorMesaage(exValidation.Message);
                BindFormModel(model);
                return View(model);
            }

            setSuccessMesaage("Kayıt başarılı");
            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            return View(userService.Get(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            userService.Delete(id);
            setSuccessMesaage("Kayıt silindi");
            return RedirectToAction("Index", "User");
        }
        private void BindFormModel(UserFormModel model)
        {
            model.RoleList = userRoleService.getList();
            model.CancelList = cancelService.getList();
            base.Model_ = model;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}