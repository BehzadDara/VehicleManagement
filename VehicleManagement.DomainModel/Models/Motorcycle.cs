using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Motorcycle : Vehicle
{
    public string TrackingCode { get; set; } = string.Empty;
    public FuelType Fuel {  get; set; }

    private Motorcycle(string title, string trackingCode, FuelType fuel)
    {
        Title = title;
        IsActive = true;
        TrackingCode = trackingCode;
        Fuel = fuel;
    }

    public static Motorcycle Create(string title, string trackingCode, FuelType fuel)
    {
        return new Motorcycle(title, trackingCode, fuel);
    }

    public void Update(string title, FuelType fuel)
    {
        Title = title;
        Fuel = fuel;
    }

    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }
}
