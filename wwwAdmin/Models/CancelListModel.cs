using DataLayer.Models;
using System;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Models
{
    public class CancelListModel : BaseListModel<Cancel>
    {
        public CancelListModel() { }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
