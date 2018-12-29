using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class AccountInfomation
    {
        [Key]
        [ForeignKey("Account")]
        public long OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Phone { get; set; }

        public AccountGender Gender { get; set; }

        public Account Account { get; set; }
    }

    public enum AccountGender
    {
        Female = 0,
        Male = 1,
        Other = 2
    }
}
