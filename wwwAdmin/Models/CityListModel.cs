using DataLayer.Models;
using System;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Models
{
    public class CityListModel : BaseListModel<City>
    {
        public CityListModel() { }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
