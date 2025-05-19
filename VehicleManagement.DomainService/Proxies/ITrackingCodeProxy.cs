namespace VehicleManagement.DomainService.Proxies;

public interface ITrackingCodeProxy
{
    public Task<List<string>> Get(int count, CancellationToken cancellationToken);
}
