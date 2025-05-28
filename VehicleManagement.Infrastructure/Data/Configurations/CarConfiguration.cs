using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.UseTpcMappingStrategy();

        builder.HasKey(x => x.Id);

        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.Title).HasMaxLength(50);

        builder.Property(x => x.TrackingCode)
            .HasConversion(
            v => EncryptionHelper.Encrypt(v),
            v => EncryptionHelper.Decrypt(v)
        ).HasMaxLength(20);

        builder.Property(x => x.Gearbox)
            .HasConversion(
            v => v.ToString(),
            v => (GearboxType)Enum.Parse(typeof(GearboxType), v)
        ).HasMaxLength(10);

        builder
            .HasMany(x => x.Options)
            .WithOne()
            .HasForeignKey("CarId").IsRequired()
            .OnDelete(DeleteBehavior.Cascade);


        builder.OwnsMany(x => x.Tags, tag =>
        {
            tag.WithOwner().HasForeignKey("CarId");
            tag.Property(x => x.Title).HasMaxLength(20);
            tag.Property(x => x.Priority).IsRequired();
            tag.HasKey("CarId", "Title", "Priority");
            tag.ToTable("CarTags");
        });

        builder.Property(x => x.CreatedBy).HasMaxLength(50);
        builder.Property(x => x.UpdatedBy).HasMaxLength(50);
        builder.Property(x => x.DeletedBy).HasMaxLength(50);
    }
}
public class CarOptionConfiguration : IEntityTypeConfiguration<CarOption>
{
    public void Configure(EntityTypeBuilder<CarOption> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Description).HasMaxLength(100);
        builder.ToTable("CarOptions");
    }
}