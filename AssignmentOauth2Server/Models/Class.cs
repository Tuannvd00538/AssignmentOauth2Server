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
        [StringLength(50)]
        public string Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [StringLength(50)]
        public string Session { get; set; }
        public int Status { get; set; }

        [Key]
        [ForeignKey("Subject")]
        public int CurrentSubjectId { get; set; }
    }
}
