using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vehicles.API.Data.Entities;

namespace Vehicles.API.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleType>().HasIndex(i => i.Description).IsUnique();
            modelBuilder.Entity<Procedure>().HasIndex(i => i.Description).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(i => i.Description).IsUnique();
            modelBuilder.Entity<Brand>().HasIndex(i => i.Description).IsUnique();
        }

    }
}
