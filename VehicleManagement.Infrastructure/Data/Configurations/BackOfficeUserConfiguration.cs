using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;

namespace VehicleManagement.Infrastructure.Data.Configurations;

internal class BackOfficeUserConfiguration : BaseEntityConfiguration<BackOfficeUser>
{
    public override void Configure(EntityTypeBuilder<BackOfficeUser> builder)
    {
        builder.Property(x => x.Username).HasMaxLength(50);
        builder.Property(x => x.Password).HasMaxLength(1000);

        builder.ToTable("BackOfficeUsers", "bo");

        builder
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey("BackOfficeUserId").IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData([
                new BackOfficeUser
                {
                    Id = 1,
                    Username = "Behzad",
                    Password = "123"
                }
            ]);

        base.Configure(builder);
    }
}

public class BackOfficeUserRoleConfiguration : BaseEntityConfiguration<BackOfficeUserRole>
{
    public override void Configure(EntityTypeBuilder<BackOfficeUserRole> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(20);
        builder.ToTable("BackOfficeUserRoles");

        builder.HasMany(x => x.Permissions)
            .WithOne()
            .HasForeignKey("BackOfficeUserRoleId")
            .OnDelete(DeleteBehavior.Cascade);

        base.Configure(builder);
    }
}