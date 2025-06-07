namespace VehicleManagement.DomainService.Proxies;

public interface ITrackingCodeProxy
{
    public int Priority { get; }
    public Task<List<string>> Get(int count, CancellationToken cancellationToken);
}
