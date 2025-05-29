using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.Models;

namespace VehicleManagement.Infrastructure.Data.Configurations;

public abstract class VehicleConfiguration<T> : TrackableEntityConfiguration<T>
    where T : Vehicle
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(50);

        base.Configure(builder);
    }
}
