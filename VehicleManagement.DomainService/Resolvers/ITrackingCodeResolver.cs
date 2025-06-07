using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.DomainService.Resolvers;

public interface ITrackingCodeResolver
{
    public ITrackingCodeProxy Resolve(int count);
}
