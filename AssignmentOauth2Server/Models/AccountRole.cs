using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentOauth2Server.Models
{
    public class AccountRole
    {
        [Key]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Key]
        [ForeignKey("Account")]
        public string AccountId { get; set; }
    }
}
