namespace VehicleManagement.DomainModel.Models.CarAggregate;

// Entity
public class CarOption
{
    public Guid Id { get; set; }
    public string Description { get; private set; }

    private CarOption(string description)
    {
        Description = description;
    }

    public static CarOption Create(string description)
    {
        return new CarOption(description);
    }
}
