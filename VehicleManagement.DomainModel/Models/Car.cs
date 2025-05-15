using VehicleManagement.DomainModel.BaseModels;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Car : TrackableEntity
{
    public string Title { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public string TrackingCode { get; private set; } = string.Empty;
    public GearboxType Gearbox { get; private set; }
}
