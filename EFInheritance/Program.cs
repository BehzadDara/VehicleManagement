using EFInheritance;
using EFInheritance.Models.TPC;
using EFInheritance.Models.TPH;
using EFInheritance.Models.TPT;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/tpc", async (MyDBContext db, CancellationToken cancellationToken) =>
{
    var dog1 = new Dog1 { Breed = "Bulldog", Name = "Test1" };
    var cat1 = new Cat1 { Lives = 5, Name = "Test2" };

    await db.AddAsync(dog1, cancellationToken);
    await db.AddAsync(cat1, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
});

app.MapPost("/tph", async (MyDBContext db, CancellationToken cancellationToken) =>
{
    var dog2 = new Dog2 { Breed = "Husky", Name = "Test3" };
    var cat2 = new Cat2 { Lives = 7, Name = "Test4" };

    await db.AddAsync(dog2, cancellationToken);
    await db.AddAsync(cat2, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
});

app.MapPost("/tpt", async (MyDBContext db, CancellationToken cancellationToken) =>
{
    var dog3 = new Dog3 { Breed = "Golden", Name = "Test5" };
    var cat3 = new Cat3 { Lives = 9, Name = "Test6" };

    await db.AddAsync(dog3, cancellationToken);
    await db.AddAsync(cat3, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
});

app.Run();
