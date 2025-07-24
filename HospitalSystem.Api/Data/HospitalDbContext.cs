using Microsoft.EntityFrameworkCore;
using HospitalSystem.Api.Models;
using YourProjectName.Models;

namespace HospitalSystem.Api.Data
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> users { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Hospital> hospitals { get; set; }
        
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<Favorite> favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
