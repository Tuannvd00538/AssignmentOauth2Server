using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class ChangePassword
    {
        public ChangePassword()
        {

        }
        public long OwnerId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
