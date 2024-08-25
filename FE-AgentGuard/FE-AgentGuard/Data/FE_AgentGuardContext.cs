using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FE_AgentGuard.Models.Models;

namespace FE_AgentGuard.Data
{
    public class FE_AgentGuardContext : DbContext
    {
        public FE_AgentGuardContext (DbContextOptions<FE_AgentGuardContext> options)
            : base(options)
        {
        }

        public DbSet<FE_AgentGuard.Models.Models.Mission> Mission { get; set; } = default!;
        public DbSet<FE_AgentGuard.Models.Models.Agent> Agent { get; set; } = default!;
        public DbSet<FE_AgentGuard.Models.Models.Target> Target { get; set; } = default!;
    }
}
