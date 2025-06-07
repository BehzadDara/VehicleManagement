using Microsoft.Extensions.DependencyInjection;
using VehicleManagement.DomainService.Proxies;
using VehicleManagement.DomainService.Resolvers;
using VehicleManagement.Infrastructure.Proxies;

namespace VehicleManagement.Infrastructure.Resolvers;

public class TrackingCodeResolver(IServiceProvider serviceProvider) : ITrackingCodeResolver
{
    // Factory pattern
    public ITrackingCodeProxy Resolve(int count)
    {
        if (count < 10)
        {
            return serviceProvider.GetRequiredService<TrackingCodeProxy>();
        }
        else
        {
            return serviceProvider.GetRequiredService<LocalTrackingCodeProxy>();
        }
    }
}
