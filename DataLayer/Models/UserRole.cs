using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    /// <summary>
    /// 1 = Site (adminden birşey yapmaya yetkisi yok)
    /// 2 = Devoloper (sistemsel kayıtları da değiştirir)
    /// 3 = Administrator
    /// </summary>
    public class UserRole
    {
        public DataPermissions Permissions { get; set; } = new DataPermissions();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string RoleName { get; set; }

        public int? CancelID { get; set; }

        [ForeignKey("CancelID")]
        public Cancel CancelObj { get; set; }
    }
}
