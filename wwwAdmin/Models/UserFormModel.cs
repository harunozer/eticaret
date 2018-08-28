using DataLayer.Models;
using System;
using System.Collections.Generic;
using wwwAdmin.Models.FormModel;

namespace wwwAdmin.Models
{
    public class UserFormModel : BaseFormModel
    {
        public User Data { get; set; }

        public List<UserRole> RoleList { get; set; }
        public List<Cancel> CancelList { get; set; }

        public override void Dispose()
        {
            if (RoleList != null)
            {
                RoleList.Clear();
                RoleList = null;
            }

            if (CancelList != null)
            {
                CancelList.Clear();
                CancelList = null;
            }

            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
