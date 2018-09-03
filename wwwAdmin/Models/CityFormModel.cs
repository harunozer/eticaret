using DataLayer.Models;
using System.Collections.Generic;
using wwwAdmin.Models.FormModel;

namespace wwwAdmin.Models
{
    public class CityFormModel : BaseFormModel<City>
    {
        public List<Country> CountryList { get; set; }
        public List<Cancel> CancelList { get; set; }

        public override void Dispose()
        {
            if (CancelList != null)
            {
                CancelList.Clear();
                CancelList = null;
            }

            if (CountryList != null)
            {
                CountryList.Clear();
                CountryList = null;
            }

            base.Dispose();
        }
    }
}
