using VehicleManagement.DomainModel.BaseModels;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Motorcycle : TrackableEntity
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public FuelType Fuel {  get; set; }
}
