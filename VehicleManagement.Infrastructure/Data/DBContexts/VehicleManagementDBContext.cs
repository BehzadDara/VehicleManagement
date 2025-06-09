using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;

namespace VehicleManagement.Infrastructure.Data.DBContexts;

public class VehicleManagementDBContext(DbContextOptions<VehicleManagementDBContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<BackOfficeUser> BackOfficeUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("v");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleManagementDBContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
