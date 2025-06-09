using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Infrastructure.Data.Configurations;

namespace VehicleManagement.Infrastructure.Data.DBContexts;

public class VehicleManagementReadDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("v");

        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.Entity<Car>().Property(x => x.Id).ValueGeneratedNever();

        modelBuilder.ApplyConfiguration(new CarOptionConfiguration());
        modelBuilder.Entity<CarOption>().Property(x => x.Id).ValueGeneratedNever();

        base.OnModelCreating(modelBuilder);
    }
}
