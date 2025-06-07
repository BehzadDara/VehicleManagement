using Microsoft.Extensions.Options;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Infrastructure.Proxies;

public class LocalTrackingCodeProxy(IOptions<Settings> options) : ITrackingCodeProxy
{
    public int Priority => options.Value.PriorityConfig.LocalTrackingCodeProxy;

    public Task<List<string>> Get(int count, CancellationToken cancellationToken)
    {
        var trackingCodes = Enumerable.Range(0, count)
            .Select(_ => $"{Random.Shared.Next(10000, 99999)}")
            .ToList();

        return Task.FromResult(trackingCodes);
    }
}
