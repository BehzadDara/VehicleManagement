using FastEndpoints;
using URLShortener;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<URLShortenerDBContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFastEndpoints();

var redisConfig = builder.Configuration.GetSection("Redis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig["HostName"];
    options.InstanceName = redisConfig["InstanceName"];
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFastEndpoints();

app.Run();
