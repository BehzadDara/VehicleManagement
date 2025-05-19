using Microsoft.Extensions.Options;
using System.Text.Json;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Infrastructure.Proxies;

public class TrackingCodeProxy(IOptions<Settings> settings, HttpClient httpClient) : ITrackingCodeProxy
{
    private readonly TrackingCodeSettings _settings = settings.Value.TrackingCode;

    public async Task<List<string>> Get(int count, CancellationToken cancellationToken)
    {
        var url = string.Format(_settings.GetUrl, _settings.Prefix, count);

        using HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("TrackingCode not available");
        }

        response.EnsureSuccessStatusCode();

        var resultString = await response.Content.ReadAsStringAsync(cancellationToken);
        var resultJson = JsonSerializer.Deserialize<GetTrackingCodeViewModel>(resultString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true})!;

        return resultJson.TrackingCodes;
    }
}
