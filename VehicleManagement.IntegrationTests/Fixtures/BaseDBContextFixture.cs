using Microsoft.EntityFrameworkCore;

namespace VehicleManagement.IntegrationTests.Fixtures;

public abstract class BaseDBContextFixture<TDBContext> : IDisposable
    where TDBContext : DbContext
{
    private TDBContext? _db;

    public TDBContext Build(string name)
    {
        var optionBuilder = new DbContextOptionsBuilder<TDBContext>().UseInMemoryDatabase(name);
        var options = optionBuilder.Options;

        _db = Build(options);
        _db.Database.EnsureCreated();

        return _db;
    }

    protected abstract TDBContext Build(DbContextOptions<TDBContext> options);

    public void Dispose()
    {
        _db?.Dispose();
        GC.SuppressFinalize(this);
    }
}
