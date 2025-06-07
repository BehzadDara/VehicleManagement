using Microsoft.Extensions.Options;
using System.Text.Json;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Infrastructure.Proxies;

public class TrackingCodeProxy : ITrackingCodeProxy
{
    private readonly Settings _settings;
    private readonly HttpClient _httpClient;

    public TrackingCodeProxy(IOptions<Settings> options, HttpClient httpClient)
    {
        _settings = options.Value;

        httpClient.BaseAddress = new Uri(_settings.TrackingCode.BaseURL);
        httpClient.DefaultRequestHeaders.Add("X-API-Key", _settings.TrackingCode.APIKey);

        _httpClient = httpClient;
    }

    public int Priority => _settings.PriorityConfig.TrackingCodeProxy;

    public async Task<List<string>> Get(int count, CancellationToken cancellationToken)
    {
        var url = string.Format(_settings.TrackingCode.GetUrl, _settings.TrackingCode.Prefix, count);

        using HttpResponseMessage response = await _httpClient.GetAsync(url, cancellationToken);

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
