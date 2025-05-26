namespace VehicleManagement.DomainModel.BaseModels;

public class TrackableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }

    public void Create(string username)
    {
        CreatedAt = DateTime.Now;
        CreatedBy = username;
    }

    public void Update(string username)
    {
        UpdatedAt = DateTime.Now;
        UpdatedBy = username;
    }

    public void Delete(string username)
    {
        DeletedAt = DateTime.Now;
        DeletedBy = username;
        IsDeleted = true;
    }
}
