using VehicleManagement.DomainModel.BaseModels;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Car : TrackableEntity
{
    public string Title { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
    public string TrackingCode { get; private set; } = string.Empty;
    public GearboxType Gearbox { get; private set; }

    private Car(string title, string trackingCode, GearboxType gearbox)
    {
        Title = title;
        IsActive = true;
        TrackingCode = trackingCode;
        Gearbox = gearbox;
    }

    public static Car Create(string title, string trackingCode, GearboxType gearbox)
    {
        return new Car(title, trackingCode, gearbox);
    }

    public void Update(string title, GearboxType gearbox)
    {
        Title = title;
        Gearbox = gearbox;
    }

    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }
}
