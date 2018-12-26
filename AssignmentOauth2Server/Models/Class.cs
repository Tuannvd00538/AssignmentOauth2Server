using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Class
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Session { get; set; }
        public int Status { get; set; }
        public int CurrentSubjectId { get; set; }
    }
}
