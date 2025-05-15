using Microsoft.Extensions.Options;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Infrastructure.Proxies;

public class TrackingCodeProxy(IOptions<Settings> settings, HttpClient httpClient) : ITrackingCodeProxy
{
    private readonly TrackingCodeSettings _settings = settings.Value.TrackingCode;

    public async Task<string> Get(CancellationToken cancellationToken)
    {
        var url = string.Format(_settings.GetUrl, _settings.Prefix);

        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("TrackingCode not available");
        }

        response.EnsureSuccessStatusCode();

        var trackingCode = await response.Content.ReadAsStringAsync(cancellationToken);
        return trackingCode;
    }
}
