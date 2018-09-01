using DataLayer.Models;
using System;
using wwwAdmin.Models.ListModel;

namespace wwwAdmin.Models
{
    public class CountryListModel : BaseListModel<Country>
    {
        public CountryListModel() { }

        public override void Dispose()
        {
            base.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
