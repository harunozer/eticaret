using DataLayer.Models.NotMapped;
using DataLayer.ValidationAttributes;
using System;

namespace DataLayer.Models
{
    public class Customer : BaseModel
    {
        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Name { get; set; }

        [ValidationRequired]
        [ValidationStringLength(50)]
        public string Surname { get; set; }

        [ValidationEmail]
        [ValidationRequired]
        [ValidationStringLength(50)]
        public string EMail { get; set; }

        [ValidationPass]
        public string Password { get; set; }

        [ValidationGsm]
        public string Gsm { get; set; }

        public DateTime DateOfBirth { get; set; }

        [ValidationGender]
        public string Gender { get; set; }
    }
}
