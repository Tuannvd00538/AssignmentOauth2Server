using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class CreateAccount
    {
        public AccountInfomation AccountInfomation { get; set; }

        public Role Role { get; set; }

        public Class Class { get; set; }
    }
}
