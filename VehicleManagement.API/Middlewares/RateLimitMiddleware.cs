using Microsoft.Extensions.Caching.Memory;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Resources;

namespace VehicleManagement.API.Middlewares;

public class RateLimitMiddleware(RequestDelegate next, IMemoryCache memoryCache)
{
    private readonly TimeSpan timeLimit = TimeSpan.FromMinutes(1);
    private readonly int countLimit = 10;

    public async Task Invoke(HttpContext context)
    {
        var key = context.Connection.RemoteIpAddress!.ToString();

        memoryCache.TryGetValue(key, out int requestCount);

        if (requestCount > countLimit)
        {
            throw new TooManyReqestException(Messages.TooManyRequest);
        }
        else
        {
            requestCount++;
            memoryCache.Set(key, requestCount, timeLimit);

            await next(context);
        }
    }
}