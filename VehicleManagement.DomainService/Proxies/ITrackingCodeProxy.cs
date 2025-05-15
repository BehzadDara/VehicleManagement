namespace VehicleManagement.DomainService.Proxies;

public interface ITrackingCodeProxy
{
    public Task<string> Get(CancellationToken cancellationToken);
}
