using FastEndpoints;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using System.Text.Json;
using System.Threading;
using URLShortener.Models;

namespace URLShortener.Endpoints;

public class RedirectEndpoint(
    URLShortenerDBContext db,
    IDistributedCache cache
    ) : EndpointWithoutRequest
{
    private readonly IMongoCollection<ShortURL> collection = db.ShortURLs;

    public override void Configure()
    {
        Get("/{code}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var code = Route<string>("code");
        var cacheKey = $"URLByCode:{code}";

        var cacheData = await cache.GetAsync(cacheKey, ct);
        if (cacheData is not null)
        {
            var result = JsonSerializer.Deserialize<string>(cacheData)!;
            await SendRedirectAsync(result, allowRemoteRedirects: true);
            return;
        }

        var shortURL = await collection.Find(x => x.Code == code).FirstOrDefaultAsync(ct);

        if (shortURL is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        cacheData = JsonSerializer.SerializeToUtf8Bytes(shortURL.OriginalURL);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5)
        };

        await cache.SetAsync(cacheKey, cacheData, options, ct);

        await SendRedirectAsync(shortURL.OriginalURL, allowRemoteRedirects: true);
    }
}
