namespace CarBlog.Models;

public class Car
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Gearbox { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
