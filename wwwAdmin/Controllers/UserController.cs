using DataLayer.Models;
using DataLayer.Services;
using System;
using System.Linq;
using wwwAdmin.Models;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Controllers
{
    public class UserController : BaseModelController<User, UserService, UserFormModel, UserListModel>
    {
        readonly UserRoleService userRoleService;
        readonly CancelService cancelService;

        public UserController(UserService userService_, UserRoleService userRoleService_, CancelService cancelService_) : base(userService_)
        {
            userRoleService = userRoleService_;
            cancelService = cancelService_;
        }

        public override void bindFilterList(UserListModel model)
        {
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
        }

        public override User getUpdatedData(UserFormModel model)
        {
            User updatedData = service.Get(model.Data.ID);
            updatedData.Name = model.Data.Name;
            updatedData.Surname = model.Data.Surname;
            updatedData.EMail = model.Data.EMail;
            updatedData.Gsm = model.Data.Gsm;
            updatedData.Password = model.Data.Password;
            updatedData.UserRoleID = model.Data.UserRoleID;
            updatedData.CancelID = model.Data.CancelID;
            return updatedData;
        }

        public override void bindFormModel(UserFormModel model)
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