using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System;

namespace DataLayer.Models
{
    public class Customer : BaseModel
    {
        public DataPermissions Permissions { get; set; } = new DataPermissions();

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Name { get; set; }

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Surname { get; set; }

        [ValidationEmail]
        [ValidationRequired]
        public string EMail { get; set; }

        [ValidationPass]
        [ValidationRequired]
        public string Password { get; set; }

        [ValidationGsm]
        public string Gsm { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ValidationGender]
        public string Gender { get; set; }

        public string LastLoginIP { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime? LastLogoutTime { get; set; }
        public int LoginCount { get; set; }
        public bool IsLogin { get; set; }
    }
}
