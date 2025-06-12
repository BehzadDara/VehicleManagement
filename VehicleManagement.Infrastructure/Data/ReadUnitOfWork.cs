using VehicleManagement.DomainService.Data;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;

namespace VehicleManagement.Infrastructure.Data;

public class ReadUnitOfWork(
    VehicleManagementReadDBContext db,
    ICarReadRepository carRepository
    ) : IReadUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await db.SaveChangesAsync(cancellationToken);
    }

    public ICarReadRepository CarRepository { get; init; } = carRepository;
}
