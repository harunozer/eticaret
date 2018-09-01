using DataLayer.Models.NotMapped;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    public class District:BaseModel
    {
        public string DistrictName { get; set; }
        public int CityID { get; set; }

        [ForeignKey("CityID")]
        public City City { get; set; }
    }
}
