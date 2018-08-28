using DataLayer.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models.NotMapped
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }

        [ValidationRequired]
        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
        public DateTime? CancelTime { get; set; }

        public User CreatedUser { get; set; }
        [ValidationRequired]
        [ForeignKey("CreatedUser")]
        public int CreatedBy { get; set; }


        public User UpdatedUser { get; set; }
        [ForeignKey("UpdatedUser")]
        public int? UpdatedBy { get; set; }


        public User CanceledUser { get; set; }
        [ForeignKey("CanceledUser")]
        public int? CanceledBy { get; set; }

        public Cancel Cancel { get; set; }
        [ForeignKey("CancelID")]
        public int? CancelID { get; set; }


        //default true. true ise createdBy vs.. SaveChanges de setlenir.
        [NotMapped]
        public bool ProcessBaseModel { get; set; } = true;


    }
}
