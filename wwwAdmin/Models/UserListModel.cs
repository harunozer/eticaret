using DataLayer.Models;
using System;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Models
{
    public class UserListModel : BaseListModel<User>
    {
        public UserListModel() { }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
