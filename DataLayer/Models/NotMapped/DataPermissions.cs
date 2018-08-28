using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.NotMapped
{
    [NotMapped]
    public class DataPermissions
    {
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool View { get; set; }
    }
}