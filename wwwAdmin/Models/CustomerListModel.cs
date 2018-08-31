using DataLayer.Models;
using System;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Models
{
    public class CustomerListModel : BaseListModel<Customer>
    {
        public CustomerListModel()
        {
        }
        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
