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
using VehicleManagement.DomainService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using VehicleManagement.API.OperationFilters;
using VehicleManagement.API;
using VehicleManagement.DomainModel.Enums;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using VehicleManagement.Application.Publishers;
using VehicleManagement.Infrastructure.Resolvers;
using VehicleManagement.DomainService.Resolvers;
using VehicleManagement.DomainService.Failovers;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<VehicleManagementDBContext>(options => options.UseSqlServer(connectionString));

var readonlyConnectionString = builder.Configuration.GetConnectionString("ReadonlyConnection");
builder.Services.AddDbContext<VehicleManagementReadonlyDBContext>(options => options.UseSqlServer(readonlyConnectionString));

var rabbitMQConfig = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddSingleton<IConnectionFactory>(_ =>
    new ConnectionFactory
    {
        HostName = rabbitMQConfig["HostName"]!,
        UserName = rabbitMQConfig["UserName"]!,
        Password = rabbitMQConfig["Password"]!,
    });

builder.Services.AddSingleton(sp =>
{
    var factory = sp.GetRequiredService<IConnectionFactory>();
    return factory.CreateConnectionAsync().Result;
});

builder.Services.AddSingleton(sp =>
{
    var connection = sp.GetRequiredService<IConnection>();
    return connection.CreateChannelAsync().Result;
});

builder.Services.AddSingleton<CarMessagePublisher>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Settings:Jwt:Issuer"],
        ValidAudience = builder.Configuration["Settings:Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Settings:Jwt:Key"]!))
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("CarModifyPolicy", policy =>
    {
        policy.RequireClaim("Permissions", PermissionType.CarModifier.ToString());
    })
    .AddPolicy("MotorcycleModifyPolicy", policy =>
    {
        policy.RequireClaim("Permissions", PermissionType.MotorcycleModifier.ToString());
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUser, CurrentUser>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();

    c.OperationFilter<AddAcceptLanguageHeaderParameter>();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IBackOfficeUserRepository, BackOfficeUserRepository>();

// Strategy pattern
builder.Services.AddScoped<TrackingCodeProxy>();
builder.Services.AddScoped<LocalTrackingCodeProxy>();
builder.Services.AddScoped<ITrackingCodeResolver, TrackingCodeResolver>();

// Failover pattern
builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();
builder.Services.AddScoped<ITrackingCodeProxy, LocalTrackingCodeProxy>();
builder.Services.AddScoped<FallbackTrackingCodeProxy>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(ValidationBehaviour<,>).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(ValidationBehaviour<,>).Assembly);

builder.Services.AddMemoryCache();

var redisConfig = builder.Configuration.GetSection("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig["HostName"];
    options.InstanceName = redisConfig["InstanceName"];
});

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddHttpClient<TrackingCodeProxy>()
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

builder.Services.AddLocalization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var supportedLanguages = Enum
    .GetValues<Languages>()
    .Cast<Languages>()
    .Select(x => x.ToString())
    .ToArray();

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedLanguages[0])
    .AddSupportedCultures(supportedLanguages)
    .AddSupportedUICultures(supportedLanguages);

app.UseRequestLocalization(localizationOptions);

app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.UseMiddleware<HttpResponseMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

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
    Cron.Yearly()
    //"*/30 * * * * *"
    );

app.Run();


// Queue -> QueueSource and QueueDestination
// Routing Key -> ModelA.Create and ModelA.Update
// Binding Key -> ModelA.Create OR ModelA.* OR *.Create
// Exchange ->
//          Direct (routing key = binding key)
//          Fanout (igonre binding key)
//          Topic (check regex of bining key)