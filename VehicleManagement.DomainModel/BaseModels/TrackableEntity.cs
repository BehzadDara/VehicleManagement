namespace VehicleManagement.DomainModel.BaseModels;

public class TrackableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; }

    public void Create()
    {
        CreatedAt = DateTime.Now;
    }

    public void Update()
    {
        UpdatedAt = DateTime.Now;
    }

    public void Delete()
    {
        DeletedAt = DateTime.Now;
        IsDeleted = true;
    }
}
