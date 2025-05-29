using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.BaseModels;

namespace VehicleManagement.Infrastructure.Data.Configurations;

public abstract class TrackableEntityConfiguration<T> : BaseEntityConfiguration<T>
    where T : TrackableEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.CreatedBy).HasMaxLength(50);
        builder.Property(x => x.UpdatedBy).HasMaxLength(50);
        builder.Property(x => x.DeletedBy).HasMaxLength(50);

        base.Configure(builder);
    }
}
