using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BE_AgentGuard.Models;

namespace BE_AgentGuard.Data
{
    public class BE_AgentGuardContext : DbContext
    {
        public BE_AgentGuardContext (DbContextOptions<BE_AgentGuardContext> options)
            : base(options)
        {
        }

        public DbSet<BE_AgentGuard.Models.Agent> Agent { get; set; } = default!;
        public DbSet<BE_AgentGuard.Models.Mission> Mission { get; set; } = default!;
        public DbSet<BE_AgentGuard.Models.Target> Target { get; set; } = default!;
    }
}
