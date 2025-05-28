using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Configurations;

public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
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

        builder.Property(x => x.Fuel)
            .HasConversion(
            v => v.ToString(),
            v => (FuelType)Enum.Parse(typeof(FuelType), v)
        ).HasMaxLength(10);

        builder.Property(x => x.CreatedBy).HasMaxLength(50);
        builder.Property(x => x.UpdatedBy).HasMaxLength(50);
        builder.Property(x => x.DeletedBy).HasMaxLength(50);
    }
}
