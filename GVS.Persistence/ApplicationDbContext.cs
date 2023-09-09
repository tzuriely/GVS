using GVS.Domain;
using GVS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GVS.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<League> Leagues { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}