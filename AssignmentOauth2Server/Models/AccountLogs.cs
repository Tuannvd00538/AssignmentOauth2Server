using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class AccountLogsMark
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int ClassId { get; set; }

        public int SubjectId { get; set; }
    }

    public class AccountLogsMail {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }

    public class AccountLogsDefault
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
    public class AccountLogs
    {
        public AccountLogs()
        {
            this.CreatedAt = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }

        [ForeignKey("Account")]
        public long OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public AccountLogsDefault Default { get; set; }

        public AccountLogsMark Mark { get; set; }

        public AccountLogsMail Mail { get; set; }

    }
}
