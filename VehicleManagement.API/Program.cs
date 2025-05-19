using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Reflection;
using Microsoft.Extensions.Options;
using VehicleManagement.Infrastructure.Data;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.DomainService.Proxies;
using VehicleManagement.Application.Behaviours;
using VehicleManagement.Infrastructure.Proxies;
using VehicleManagement.DomainService;
using VehicleManagement.API.Middlewares;
using VehicleManagement.API.Features;
using VehicleManagement.DomainModel.Attributes;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Data.Repositories;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainModel.BaseModels;
using Hangfire;
using Hangfire.MemoryStorage;
using VehicleManagement.Application.Jobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleManagementDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();

builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);

builder.Services.AddMemoryCache();

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<Settings>>().Value.TrackingCode;
    client.BaseAddress = new Uri(settings.BaseURL);
})
.AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
        onRetry: (response, delay, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {delay.TotalSeconds}s due to {response.Exception?.Message ?? response.Result?.StatusCode.ToString()}");
        }));

builder.Services.AddTransient<CarsTrackingCodeJob>();

builder.Services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});

builder.Services.AddHangfireServer(options =>
{
    options.SchedulePollingInterval = TimeSpan.FromSeconds(10);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();

var enumTypes = typeof(BaseEntity).Assembly
    .GetTypes()
    .Where(t => t.IsEnum && t.GetCustomAttributes(typeof(EnumEndpointAttribute), false).Length != 0)
    .ToList();

foreach (var enumType in enumTypes)
{
    var attribute = (enumType.GetCustomAttribute(typeof(EnumEndpointAttribute)) as EnumEndpointAttribute)!;
    var route = attribute.Route;

    app.MapGet(route, () =>
    {
        var enumValues = Enum.GetValues(enumType)
                         .Cast<Enum>()
                         .Select(e => new EnumViewModel(e));

        return BaseResult.Success(enumValues);
    })
        .WithTags("Enums");
}

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<CarsTrackingCodeJob>(
    "get-cars-tracking-code-job",
    job => job.Get(),
    "*/30 * * * * *"
    );

app.Run();