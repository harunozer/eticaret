using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class City : BaseModel
    {
        [ValidationRequired]
        [ValidationStringLength(50)]
        public string CityName { get; set; }

        public int CounrtyID { get; set; }

        [ForeignKey("CounrtyID")]
        public Country Country { get; set; }
    }
}
