using VehicleManagement.DomainService.Repositories;

namespace VehicleManagement.DomainService.Data;

public interface IReadUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
    public ICarReadRepository CarRepository { get; init; }
}
