using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models
{
    /// <summary>
    /// ID = 1 Site Kullanıcısı (Rol = 1)
    /// </summary>
    public class User : BaseModel
    {
        public DataPermissions Permissions { get; set; } = new DataPermissions();

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Name { get; set; }

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Surname { get; set; }

        [ValidationRequired]
        [ValidationEmail]
        public string EMail { get; set; }

        [ValidationPass]
        [ValidationRequired]
        public string Password { get; set; }

        [ValidationRequired]
        public int UserRoleID { get; set; }

        [ForeignKey("UserRoleID")]
        public UserRole UserRole { get; set; }

        public string LastLoginIP { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public int LoginCount { get; set; }
        public bool IsLogin { get; set; }

        [ValidationGsm]
        [ValidationStringLength(20)]
        public string Gsm { get; set; }
    }
}
