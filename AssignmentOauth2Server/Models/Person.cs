using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Person
    {
        [Key]
        [StringLength(50)]
        [ForeignKey("Account")]
        public string AccountId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public string Avatar { get; set; }

        public Account Account { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 0
    }
}
