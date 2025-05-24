namespace VehicleManagement.DomainModel.Models.CarAggregate;

// Value type
public class CarTag
{
    public string Title { get; private set; } = string.Empty;
    public int Priority { get; private set; }

    private CarTag(string title, int priority)
    {
        Title = title;
        Priority = priority;
    }

    public static CarTag Create(string title, int priority)
    {
        return new CarTag(title, priority);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (obj is not CarTag tag) return false;

        return Title == tag.Title && Priority == tag.Priority;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Priority);
    }
}
