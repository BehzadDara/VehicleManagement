using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.API.DTOs;

public class CreateOrUpdateCarDTO
{
    public required string Title { get; set; }
    public required GearboxType Gearbox { get; set; }
}
