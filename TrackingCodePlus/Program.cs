using Microsoft.AspNetCore.Mvc;
using TrackingCodePlus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/TrackingCodes/{prefix}/{count}", ([FromRoute] string prefix, [FromRoute] int count = 1) =>
{
    var trackingCodes = Enumerable.Range(0, count)
        .Select(_ => $"{Random.Shared.Next(10000, 99999)}-{prefix}")
        .ToList();

    var result = new GetViewModel { TrackingCodes = trackingCodes };

    return result;
});

app.UseMiddleware<APIKeyMiddleware>();

app.Run();
