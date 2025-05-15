using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.API.DTOs;

public class CreateOrUpdateMotorcycleDTO
{
    public required string Title { get; set; }
    public required FuelType Fuel { get; set; }
}