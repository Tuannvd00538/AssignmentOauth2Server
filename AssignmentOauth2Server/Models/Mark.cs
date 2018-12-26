using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Mark
    {
        public long Id { get; set; }
        public int SubjectId { get; set; }
        public string AccountId { get; set; }
        public float Value { get; set; }
        public int MarkType { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
    }
}
