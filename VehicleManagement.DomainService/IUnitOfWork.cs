using VehicleManagement.DomainService.Repositories;

namespace VehicleManagement.DomainService;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);

    public ICarRepository CarRepository { get; init; }
    public IMotorcycleRepository MotorcycleRepository { get; init; }
    public IBackOfficeUserRepository BackOfficeUserRepository { get; init; }
}
