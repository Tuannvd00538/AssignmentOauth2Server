using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AssignmentOauth2Server.Models
{
    public class AssignmentOauth2ServerContext : DbContext
    {
        public AssignmentOauth2ServerContext (DbContextOptions<AssignmentOauth2ServerContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }

        public DbSet<AccountInfomation> AccountInfomation { get; set; }

        public DbSet<Credential> Credential { get; set; }
    }
}
