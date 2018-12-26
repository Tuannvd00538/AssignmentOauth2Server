using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Account
    {

        public Account()
        {
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Status = StatusAccount.Active;
        }

        [Key]
        [StringLength(50)]
        public string Id { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(200)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Salt { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime DeletedAt { get; set; }
        public StatusAccount Status { get; set; }

        public Person AccountInfomation { get; set; }
    }

    public enum StatusAccount
    {
        Active = 1,
        Deactive = 0
    }
}
