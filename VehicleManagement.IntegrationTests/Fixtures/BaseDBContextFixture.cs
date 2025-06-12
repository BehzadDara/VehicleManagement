using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.IntegrationTests.Fixtures;

public abstract class BaseDBContextFixture<TDBContext> : IDisposable
    where TDBContext : DbContext
{
    public TDBContext Build(string name)
    {
        var optionBuilder = new DbContextOptionsBuilder<TDBContext>().UseInMemoryDatabase(name);
        var options = optionBuilder.Options;

        var db = Build(options);
        db.Database.EnsureCreated();

        return db;
    }

    protected abstract TDBContext Build(DbContextOptions<TDBContext> options);

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
