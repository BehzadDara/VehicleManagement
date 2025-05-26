using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;

public class BackOfficeUserPermission
{
    public int Id { get; set; }
    public PermissionType Type { get; set; }
}
