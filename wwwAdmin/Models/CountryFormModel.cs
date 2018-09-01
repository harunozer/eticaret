using DataLayer.Models;
using System;
using System.Collections.Generic;
using wwwAdmin.Models.FormModel;

namespace wwwAdmin.Models
{
    public class CountryFormModel : BaseFormModel<Country>
    {
        public List<Cancel> CancelList { get; set; }

        public override void Dispose()
        {
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
