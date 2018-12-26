using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Mark
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }

        [StringLength(50)]
        [ForeignKey("Account")]
        public string AccountId { get; set; }
        public float Value { get; set; }
        public int MarkType { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
