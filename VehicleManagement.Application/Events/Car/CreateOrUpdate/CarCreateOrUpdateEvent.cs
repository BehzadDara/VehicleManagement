using MediatR;

namespace VehicleManagement.Application.Events.Car.CreateOrUpdate;

public class CarCreateOrUpdateEvent : INotification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Gearbox { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
