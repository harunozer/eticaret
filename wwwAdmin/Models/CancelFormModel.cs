using DataLayer.Models;
using System.Collections.Generic;
using wwwAdmin.Models.FormModel;

namespace wwwAdmin.Models
{
    public class CancelFormModel : BaseFormModel<Cancel>
    {
        public List<Cancel> CancelList { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
