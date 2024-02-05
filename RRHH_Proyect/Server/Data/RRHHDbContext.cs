using Microsoft.EntityFrameworkCore;

namespace RRHH_Proyect.Server.Data
{
    public class RRHHDbContext : DbContext
    {
        public RRHHDbContext(DbContextOptions<RRHHDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
