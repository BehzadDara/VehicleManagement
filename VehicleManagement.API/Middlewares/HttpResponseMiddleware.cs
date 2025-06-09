using VehicleManagement.Application.Exceptions;
using VehicleManagement.Resources;

namespace VehicleManagement.API.Middlewares;

public class HttpResponseMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        await next(context);

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized: throw new UnauthorizedException(Resources.Messages.Unauthorized);
            case StatusCodes.Status403Forbidden: throw new ForbiddenException(Resources.Messages.Forbidden);
        }
    }
}
