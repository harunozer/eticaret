using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;

namespace DataLayer.Models
{
    public class Country : BaseModel
    {
        public DataPermissions Permissions { get; set; } = new DataPermissions();

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string CountryName { get; set; }
    }
}
