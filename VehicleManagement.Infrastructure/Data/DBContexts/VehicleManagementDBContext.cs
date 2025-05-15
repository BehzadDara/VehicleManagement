using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.DBContexts;

public class VehicleManagementDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("v");

        #region Car

        modelBuilder.Entity<Car>().HasKey(x => x.Id);

        modelBuilder.Entity<Car>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Car>().Property(x => x.Title).HasMaxLength(50);

        modelBuilder.Entity<Car>().Property(x => x.TrackingCode)
            .HasConversion(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v)
        ).HasMaxLength(20);

        modelBuilder.Entity<Car>().Property(x => x.Gearbox)
            .HasConversion(
            v => v.ToString(),
            v => (GearboxType)Enum.Parse(typeof(GearboxType), v)
        ).HasMaxLength(10);

        #endregion

        #region Motorcycle

        modelBuilder.Entity<Motorcycle>().HasKey(x => x.Id);

        modelBuilder.Entity<Motorcycle>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Motorcycle>().Property(x => x.Title).HasMaxLength(50);

        modelBuilder.Entity<Motorcycle>().Property(x => x.TrackingCode)
            .HasConversion(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v)
        ).HasMaxLength(20);

        modelBuilder.Entity<Motorcycle>().Property(x => x.Fuel)
            .HasConversion(
            v => v.ToString(),
            v => (FuelType)Enum.Parse(typeof(FuelType), v)
        ).HasMaxLength(10);

        #endregion


        base.OnModelCreating(modelBuilder);
    }
}
