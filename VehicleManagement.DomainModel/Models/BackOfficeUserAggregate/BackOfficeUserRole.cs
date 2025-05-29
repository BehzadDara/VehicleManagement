using VehicleManagement.DomainModel.BaseModels;

namespace VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;

public class BackOfficeUserRole : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public List<BackOfficeUserPermission> Permissions { get; set; } = [];
}
