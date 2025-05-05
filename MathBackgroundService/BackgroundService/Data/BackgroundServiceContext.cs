using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackgroundServiceVote.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BackgroundServiceVote.Data
{
    public class BackgroundServiceContext : IdentityDbContext
    {
        public BackgroundServiceContext (DbContextOptions<BackgroundServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Player { get; set; } = default!;
    }
}
