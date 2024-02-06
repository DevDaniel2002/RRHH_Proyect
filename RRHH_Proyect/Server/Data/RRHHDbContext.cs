using Microsoft.EntityFrameworkCore;
using RRHH_Proyect.Shared;

namespace RRHH_Proyect.Server.Data
{
    public class RRHHDbContext : DbContext
    {
        public RRHHDbContext(DbContextOptions<RRHHDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainings>()
            .HasOne(t => t.TrainingType)
            .WithMany()
            .HasForeignKey(t => t.TrainingTypeId);
        }

        public DbSet<TrainingTypes> TrainingTypes { get; set; }
        public DbSet<Trainings> Trainings { get; set; }
        public DbSet<WorkExperience> WorkExperience { get; set; }
        


    }
}
