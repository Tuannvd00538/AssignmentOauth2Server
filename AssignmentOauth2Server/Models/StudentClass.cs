using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class StudentClass
    {
        [Key]
        [StringLength(50)]
        [ForeignKey("Class")]
        public string ClassId { get; set; }

        [Key]
        [StringLength(50)]
        [ForeignKey("Account")]
        public string AccountId { get; set; }
    }
}
