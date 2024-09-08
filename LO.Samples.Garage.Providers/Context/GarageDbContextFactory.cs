using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LO.Samples.Garage.Providers.Context
{
    public class GarageDbContextFactory : IDesignTimeDbContextFactory<GarageDbContext> 
    {
        public GarageDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<GarageDbContext>();
            optionsBuilder.UseSqlServer("{enter your connection string here}");

            return new GarageDbContext(optionsBuilder.Options);
        }
    }
}
