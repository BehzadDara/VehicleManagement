using VehicleManagement.DomainModel.BaseModels;

namespace VehicleManagement.DomainModel.Models;

public class Vehicle : TrackableEntity
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
