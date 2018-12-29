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

        public float Value { get; set; }

        public MarkType MarkType { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public MarkStatus Status { get; set; }

        [Key]
        [ForeignKey("Account")]
        public long OwnerId { get; set; }

        [Key]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
    }

    public enum MarkType
    {
        Theory = 0,
        Practice = 1,
        Assignment = 3
    }

    public enum MarkStatus
    {
        Active = 1,
        Deactive = 0
    }
}
