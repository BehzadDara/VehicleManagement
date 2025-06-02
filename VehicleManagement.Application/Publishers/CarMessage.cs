namespace VehicleManagement.Application.Publishers;

public class CarMessage
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Gearbox { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
