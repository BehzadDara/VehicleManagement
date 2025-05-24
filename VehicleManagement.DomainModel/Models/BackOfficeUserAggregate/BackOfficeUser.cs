using VehicleManagement.DomainModel.BaseModels;

namespace VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;

public class BackOfficeUser : BaseEntity
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public DateTime? LastLoginAt { get; private set; }

    public void SetLastLoginAt()
    {
        LastLoginAt = DateTime.Now;
    }
}
