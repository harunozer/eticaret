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
        public DataPermissions Permissions { get; set; } = new DataPermissions();


        [ValidationRequired]
        [ValidationStringLength(50)]
        public string CityName { get; set; }

        public int CountryID { get; set; }

        [ForeignKey("CountryID")]
        public Country Country { get; set; }
    }
}
