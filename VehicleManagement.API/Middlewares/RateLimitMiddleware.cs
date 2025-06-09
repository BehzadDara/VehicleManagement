using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.API.Middlewares;

public class RateLimitMiddleware(RequestDelegate next, IDistributedCache cache, ICurrentUser currentUser)
{
    private readonly TimeSpan timeLimit = TimeSpan.FromMinutes(1);
    private readonly int countLimit = 1000;

    public async Task Invoke(HttpContext context)
    {
        var cacheKey = $"RateLimit:{currentUser.IPAddress}";

        var cacheData = await cache.GetAsync(cacheKey);

        var requestCount = 0;
        try
        {
            requestCount = JsonSerializer.Deserialize<int>(cacheData);
        }
        catch { }

        if (requestCount > countLimit)
        {
            throw new TooManyReqestException(Resources.Messages.TooManyRequest);
        }
        else
        {
            requestCount++;

            cacheData = JsonSerializer.SerializeToUtf8Bytes(requestCount);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeLimit
            };

            await cache.SetAsync(cacheKey, cacheData, options);

            await next(context);
        }
    }
}