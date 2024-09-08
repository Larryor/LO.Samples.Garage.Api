using LO.Samples.Garage.Providers.Tables;
using Microsoft.EntityFrameworkCore;

namespace LO.Samples.Garage.Providers.Context
{
    public class GarageDbContext : DbContext
    {
        public GarageDbContext(DbContextOptions<GarageDbContext> options)
            : base(options)
        {
        }

        public DbSet<BranchTable> Branches { get; set; } = null!;

        public DbSet<VehicleTable> Vehicles { get; set; } = null!;

        public DbSet<CustomerTable> Customers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BranchTable>().HasData(new BranchTable
            {
                Id = Guid.Parse("110a9e97-9477-41d1-b4aa-98c2b999e34c"),
                Name = "DefaultBranch1",
                CreatedAtUTC = DateTime.Parse("2024-09-07 15:09:08.0000000"),
                LastModifiedAtUTC = DateTime.Parse("2024-09-07 15:09:08.0000000"),
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
