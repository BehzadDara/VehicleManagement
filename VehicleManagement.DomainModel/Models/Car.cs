using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Car : Vehicle
{
    public string TrackingCode { get; private set; } = string.Empty;
    public GearboxType Gearbox { get; private set; }

    private Car(string title, GearboxType gearbox)
    {
        Title = title;
        IsActive = true;
        Gearbox = gearbox;
    }

    public static Car Create(string title, GearboxType gearbox)
    {
        return new Car(title, gearbox);
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

    public void SetTrackingCode(string trackingCode)
    {
        TrackingCode = trackingCode;
    }
}
