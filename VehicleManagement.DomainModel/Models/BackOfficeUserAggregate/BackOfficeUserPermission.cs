using VehicleManagement.DomainModel.BaseModels;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;

public class BackOfficeUserPermission : BaseEntity
{
    public PermissionType Type { get; set; }
}
