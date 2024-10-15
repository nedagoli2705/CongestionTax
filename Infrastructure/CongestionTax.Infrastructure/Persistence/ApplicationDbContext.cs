using CongestionTax.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CongestionTax.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TollPass> TollPasses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle ( 1, "Car", false ),
            new Vehicle ( 2, "Motorbike", true ),
            new Vehicle ( 3, "Bus", true ),
            new Vehicle ( 4, "Emergency", true ),
            new Vehicle ( 5, "Diplomat", true ),
            new Vehicle ( 6, "Military", true ),
            new Vehicle ( 7, "Foreign", true ));
        }
    }
}
