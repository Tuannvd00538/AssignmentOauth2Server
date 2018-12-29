using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class SubjectClass
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        
        [ForeignKey("Class")]
        public string ClassId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime IntendTime { get; set; }

        public ProcessStatus Status { get; set; }
    }

    public enum ProcessStatus
    {
        Pending = 0,
        Success = 1,
        Delay = 3
    }
}
