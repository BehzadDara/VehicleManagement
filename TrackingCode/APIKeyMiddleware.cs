namespace TrackingCode;

public class APIKeyMiddleware(RequestDelegate next)
{
    private readonly List<string> AvailableAPIKeys = ["VehicleManagementAPIKey"];

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("X-API-Key", out var apikey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("APIKey was not provided");
            return;
        }

        if (!AvailableAPIKeys.Contains(apikey!))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("APIKey was not authorized");
            return;
        }

        await next(context);

    }
}