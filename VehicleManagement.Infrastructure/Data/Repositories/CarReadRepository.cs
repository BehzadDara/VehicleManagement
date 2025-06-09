using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class CarReadRepository(VehicleManagementReadDBContext db) : ICarReadRepository
{
    private readonly DbSet<Car> set = db.Set<Car>();

    public async Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        await set.AddAsync(car, cancellationToken);
    }

    public void Update(Car car)
    {
        set.Update(car);
    }

    public void Delete(Car car)
    {
        set.Remove(car);
    }

    public async Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await set.AsNoTracking()
            .Include(x => x.Options)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
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
