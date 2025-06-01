    using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class CarRepository(VehicleManagementDBContext db, ICurrentUser currentUser) : ICarRepository
{
    private readonly DbSet<Car> set = db.Set<Car>();

    private IQueryable<Car> GetQueryable(IQueryable<Car> query)
    {
        if (currentUser.HasGodAccess)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    private IQueryable<Car> GetGeneralQueryable()
    {
        var query = set.AsQueryable();

        return GetQueryable(query);
    }

    private IQueryable<Car> GetReadOnlyQueryable()
    {
        var query = set.AsNoTracking().AsQueryable();

        return GetQueryable(query);
    }

    public async Task AddAsync(Car car, CancellationToken cancellationToken)
    {
        car.Create(currentUser.Username);

        await set.AddAsync(car, cancellationToken);
    }

    public void Update(Car car)
    {
        car.Update(currentUser.Username);

        set.Update(car);
    }

    public void Update(List<Car> cars)
    {
        set.UpdateRange(cars);
    }

    public void Delete(Car car)
    {
        car.Delete(currentUser.Username);

        set.Update(car);
    }

    public async Task<Car?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await GetGeneralQueryable()
            .Include(x => x.Options)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(int, List<Car>)> GetListAsync(BaseSpecification<Car> specification, CancellationToken cancellationToken)
    {
        var query = GetReadOnlyQueryable().Specify(specification);

        var totalCount = await query.CountAsync(cancellationToken);

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        var result = await query.ToListAsync(cancellationToken);

        return (totalCount, result);
    }
}
