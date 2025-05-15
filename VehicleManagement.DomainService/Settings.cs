namespace Shirin.DomainService;

public class Settings
{
    public required TrackingCodeSettings TrackingCode {  get; set; }
}

public class TrackingCodeSettings
{
    public required string BaseURL { get; set; }
    public required string GetUrl { get; set; }
    public required string Prefix { get; set; }
}