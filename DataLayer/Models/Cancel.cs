using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    /// <summary>
    /// ID < 0 olan kayıtlar adminde de gösterilmez
    /// </summary>
    public class Cancel
    {
        public DataPermissions Permissions { get; set; } = new DataPermissions();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string CancelName { get; set; }

        public int? CancelID { get; set; }

        /// <summary>
        /// İlişkiler manuekl tablolar oluşturulduktan sonra oluşturuluyor
        /// </summary>
        [ForeignKey("CancelID")]
        public Cancel CancelObj { get; set; }
        
    }
}
