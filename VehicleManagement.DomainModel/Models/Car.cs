using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

// Aggregate root
public class Car : Vehicle
{
    public string TrackingCode { get; private set; } = string.Empty;
    public GearboxType Gearbox { get; private set; }
    public List<CarOption> Options { get; private set; } = [];
    public List<CarTag> Tags { get; private set; } = [];

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

    public void AddOption(string description)
    {
        var option = CarOption.Create(description);
        Options.Add(option);
    }

    public void RemoveOption(Guid optionId)
    {
        var option = Options.FirstOrDefault(x => x.Id == optionId);
        if (option != null)
        {
            Options.Remove(option);
        }
    }

    public void AddTag(string title, int priority)
    {
        var tag = CarTag.Create(title, priority);
        Tags.Add(tag);
    }

    public void RemoveTag(string title, int priority)
    {
        var tag = CarTag.Create(title, priority);
        Tags.Remove(tag);
    }
}
