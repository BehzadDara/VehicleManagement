using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.DBContexts;

public class VehicleManagementDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Motorcycle> Motorcycles { get; set; }
    public DbSet<BackOfficeUser> BackOfficeUsers { get; set; }

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

        modelBuilder.Entity<Car>().Property(x => x.CreatedBy).HasMaxLength(50);
        modelBuilder.Entity<Car>().Property(x => x.UpdatedBy).HasMaxLength(50);
        modelBuilder.Entity<Car>().Property(x => x.DeletedBy).HasMaxLength(50);

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

        modelBuilder.Entity<Motorcycle>().Property(x => x.Fuel)
            .HasConversion(
            v => v.ToString(),
            v => (FuelType)Enum.Parse(typeof(FuelType), v)
        ).HasMaxLength(10);

        modelBuilder.Entity<Motorcycle>().Property(x => x.CreatedBy).HasMaxLength(50);
        modelBuilder.Entity<Motorcycle>().Property(x => x.UpdatedBy).HasMaxLength(50);
        modelBuilder.Entity<Motorcycle>().Property(x => x.DeletedBy).HasMaxLength(50);

        #endregion

        #region BackOfficeUser

        modelBuilder.Entity<BackOfficeUser>().HasKey(x => x.Id);

        modelBuilder.Entity<BackOfficeUser>().Property(x => x.Username).HasMaxLength(50);
        modelBuilder.Entity<BackOfficeUser>().Property(x => x.Password).HasMaxLength(1000);

        modelBuilder.Entity<BackOfficeUser>().ToTable("BackOfficeUsers", "bo");

        modelBuilder.Entity<BackOfficeUser>()
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey("BackOfficeUserId").IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BackOfficeUserRole>().HasKey(x => x.Id);
        modelBuilder.Entity<BackOfficeUserRole>().Property(x => x.Name).HasMaxLength(20);
        modelBuilder.Entity<BackOfficeUserRole>().ToTable("BackOfficeUserRoles");

        modelBuilder.Entity<BackOfficeUserRole>()
            .HasMany(x => x.Permissions)
            .WithOne()
            .HasForeignKey("BackOfficeUserRoleId").IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BackOfficeUserPermission>().HasKey(x => x.Id);
        modelBuilder.Entity<BackOfficeUserPermission>().ToTable("BackOfficeUserPermissions");

        modelBuilder.Entity<BackOfficeUser>().HasData([
                new BackOfficeUser
                { 
                    Id = 1, 
                    Username = "Behzad", 
                    Password = "123"
                }
            ]);

        #endregion


        base.OnModelCreating(modelBuilder);
    }
}
