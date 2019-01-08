using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentOauth2Server.Controllers;
using AssignmentOauth2Server.Models;

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

        public DbSet<Login> Login { get; set; }

        public DbSet<AssignmentOauth2Server.Models.Class> Class { get; set; }

        public DbSet<AssignmentOauth2Server.Models.Subject> Subject { get; set; }

        public DbSet<AssignmentOauth2Server.Models.Classes> Classes { get; set; }

        public DbSet<AssignmentOauth2Server.Models.Role> Role { get; set; }

        public DbSet<AssignmentOauth2Server.Models.AccountLogs> Log { get; set; }

        public DbSet<AssignmentOauth2Server.Models.AccountLogsDefault> Default { get; set; }

        public DbSet<AssignmentOauth2Server.Models.AccountLogsMail> Mail { get; set; }

        public DbSet<AssignmentOauth2Server.Models.AccountLogsMark> Mark { get; set; }
        public DbSet<AssignmentOauth2Server.Models.Mark> MarkCurrent { get; set; }
        public DbSet<AssignmentOauth2Server.Models.SubjectClass> SubjectClass { get; set; }
    }
}
