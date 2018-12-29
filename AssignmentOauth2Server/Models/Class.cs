using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime IntendTime { get; set; }

        public ClassStatus Status { get; set; }
        
        [ForeignKey("Subject")]
        public int CurrentSubjectId { get; set; }
    }

    public enum ClassStatus
    {
        Active = 1,
        Deactive = 0
    }
}
