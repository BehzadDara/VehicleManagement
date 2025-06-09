using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.DBContexts;

public class VehicleManagementReadonlyDBContext(DbContextOptions<VehicleManagementReadonlyDBContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("v");

        modelBuilder.Entity<Car>().HasKey(x => x.Id);

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
            .HasForeignKey("CarId").IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Car>().OwnsMany(x => x.Tags, tag =>
        {
            tag.WithOwner().HasForeignKey("CarId");
            tag.Property(x => x.Title).HasMaxLength(20);
            tag.Property(x => x.Priority).IsRequired();
            tag.HasKey("CarId", "Title", "Priority");
            tag.ToTable("CarTags");
        });

        modelBuilder.Entity<Car>().HasQueryFilter(x => !x.IsDeleted);

        modelBuilder.Entity<Car>().Property(x => x.CreatedBy).HasMaxLength(50);
        modelBuilder.Entity<Car>().Property(x => x.UpdatedBy).HasMaxLength(50);
        modelBuilder.Entity<Car>().Property(x => x.DeletedBy).HasMaxLength(50);

        base.OnModelCreating(modelBuilder);
    }
}
