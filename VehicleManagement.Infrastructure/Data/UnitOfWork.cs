using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;

namespace VehicleManagement.Infrastructure.Data;

public class UnitOfWork(
    VehicleManagementDBContext db,
    ICarRepository carRepository,
    ICarReadRepository carReadRepository,
    IMotorcycleRepository motorcycleRepository,
    IBackOfficeUserRepository backOfficeUserRepository
    ) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await db.SaveChangesAsync(cancellationToken);
    }

    public ICarRepository CarRepository { get; init; } = carRepository;
    public ICarReadRepository CarReadRepository { get; init; } = carReadRepository;
    public IMotorcycleRepository MotorcycleRepository { get; init; } = motorcycleRepository;
    public IBackOfficeUserRepository BackOfficeUserRepository { get; init; } = backOfficeUserRepository;
}
