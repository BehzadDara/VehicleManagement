using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/TrackingCodes/{prefix}", ([FromRoute] string prefix) =>
{
    return $"{prefix}-{Random.Shared.Next(10000, 99999)}";
});

app.Run();
