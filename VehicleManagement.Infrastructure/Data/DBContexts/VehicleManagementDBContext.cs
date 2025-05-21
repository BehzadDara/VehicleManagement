using Microsoft.EntityFrameworkCore;
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

        modelBuilder.Entity<Car>().UseTpcMappingStrategy();

        modelBuilder.Entity<Car>().HasKey(x => x.Id);

        modelBuilder.Entity<Car>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Car>().Property(x => x.Title).HasMaxLength(50);

        modelBuilder.Entity<Car>().Property(x => x.TrackingCode)
            .HasConversion(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v)
        ).HasMaxLength(20);

        modelBuilder.Entity<Car>()
            .HasIndex(x => x.TrackingCode)
            .IsUnique();

        modelBuilder.Entity<Car>().Property(x => x.Gearbox)
            .HasConversion(
            v => v.ToString(),
            v => (GearboxType)Enum.Parse(typeof(GearboxType), v)
        ).HasMaxLength(10);

        modelBuilder.Entity<Car>()
            .HasMany(x => x.Options)
            .WithOne()
            .HasForeignKey("CarId").IsRequired() // Shadow property
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CarOption>().HasKey(x => x.Id);
        modelBuilder.Entity<CarOption>().Property(x => x.Description).HasMaxLength(100);
        modelBuilder.Entity<CarOption>().ToTable("CarOptions");

        modelBuilder.Entity<Car>()
            .OwnsMany(x => x.Tags, tag =>
            {
                tag.WithOwner().HasForeignKey("CarId");
                tag.Property(x => x.Title).HasMaxLength(20);
                tag.Property(x => x.Priority).IsRequired();
                tag.HasKey("CarId", "Title", "Priority");
                tag.ToTable("CarTags");
            });

        #endregion

        #region Motorcycle

        modelBuilder.Entity<Motorcycle>().UseTpcMappingStrategy();

        modelBuilder.Entity<Motorcycle>().HasKey(x => x.Id);

        modelBuilder.Entity<Motorcycle>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Motorcycle>().Property(x => x.Title).HasMaxLength(50);

        modelBuilder.Entity<Motorcycle>().Property(x => x.TrackingCode)
            .HasConversion(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v)
        ).HasMaxLength(20);

        modelBuilder.Entity<Motorcycle>()
            .HasIndex(x => x.TrackingCode)
            .IsUnique();

        modelBuilder.Entity<Motorcycle>().Property(x => x.Fuel)
            .HasConversion(
            v => v.ToString(),
            v => (FuelType)Enum.Parse(typeof(FuelType), v)
        ).HasMaxLength(10);

        #endregion


        base.OnModelCreating(modelBuilder);
    }
}
