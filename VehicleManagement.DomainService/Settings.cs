namespace VehicleManagement.DomainService;

public class Settings
{
    public required TrackingCodeSettings TrackingCode {  get; set; }
    public required JWTSettings JWT {  get; set; }
    public required List<string> GodUsers { get; set; } = [];
    public required PriorityConfigSettings PriorityConfig { get; set; }
}

public class TrackingCodeSettings
{
    public required string BaseURL { get; set; }
    public required string GetUrl { get; set; }
    public required string Prefix { get; set; }
    public required string APIKey { get; set; }
}

public class JWTSettings
{
    public required string Key { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int ExpireInMinutes { get; set; }
}

public class PriorityConfigSettings
{
    public int TrackingCodeProxy { get; set; }
    public int LocalTrackingCodeProxy { get; set; }
    public int FallbackTrackingCodeProxy { get; set; }
}