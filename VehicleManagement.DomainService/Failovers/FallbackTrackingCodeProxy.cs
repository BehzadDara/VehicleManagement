using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.DomainService.Failovers;

public class FallbackTrackingCodeProxy(
    IEnumerable<ITrackingCodeProxy> trackingCodeProxies
    ) : ITrackingCodeProxy
{
    public int Priority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Chain of Responsibility pattern
    public async Task<List<string>> Get(int count, CancellationToken cancellationToken)
    {
        foreach (var trackingCodeProxy in trackingCodeProxies.OrderBy(x => x.Priority))
        {
            try
            {
                return await trackingCodeProxy.Get(count, cancellationToken);
            }
            catch
            {
                // Log
            }
        }

        throw new Exception("TrackingCode Services not available");
    }
}
