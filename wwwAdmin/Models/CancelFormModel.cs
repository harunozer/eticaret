using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wwwAdmin.Models.FormModel;

namespace wwwAdmin.Models
{
    public class CancelFormModel : BaseFormModel
    {
        public Cancel Data { get; set; }
        public List<Cancel> CancelList { get; set; }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
