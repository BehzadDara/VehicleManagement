using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class CarRepository(VehicleManagementDBContext db) : ICarRepository
{
    private readonly DbSet<Car> set = db.Set<Car>();

    public async Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        car.Create();

        await set.AddAsync(car, cancellationToken);
    }

    public void Update(Car car)
    {
        car.Update();

        set.Update(car);
    }

    public void Delete(Car car)
    {
        car.Delete();

        set.Update(car);
    }

    public async Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(int, List<Car>)> GetListAsync(BaseSpecification<Car> specification, CancellationToken cancellationToken)
    {
        var query = set.AsNoTracking().Specify(specification);

        var totalCount = await query.CountAsync(cancellationToken);

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        var result = await query.ToListAsync(cancellationToken);

        return (totalCount, result);
    }
}
