using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Configurations;

public class MotorcycleConfiguration : VehicleConfiguration<Motorcycle>
{
    public override void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.UseTpcMappingStrategy();

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

        base.Configure(builder);
    }
}
