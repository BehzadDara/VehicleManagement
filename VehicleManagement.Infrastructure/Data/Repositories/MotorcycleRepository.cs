using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class MotorcycleRepository(VehicleManagementDBContext db, ICurrentUser currentUser) : IMotorcycleRepository
{

    private readonly DbSet<Motorcycle> set = db.Set<Motorcycle>();

    private IQueryable<Motorcycle> GetQueryable(IQueryable<Motorcycle> query)
    {
        if (currentUser.HasGodAccess)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    private IQueryable<Motorcycle> GetReadOnlyQueryable()
    {
        var query = set.AsNoTracking().AsQueryable();

        return GetQueryable(query);
    }

    public async Task AddAsync(Motorcycle motorcycle, CancellationToken cancellationToken)
    {
        motorcycle.Create(currentUser.Username);

        await set.AddAsync(motorcycle, cancellationToken);
    }

    public void Update(Motorcycle motorcycle)
    {
        motorcycle.Update(currentUser.Username);

        set.Update(motorcycle);
    }

    public void Delete(Motorcycle motorcycle)
    {
        motorcycle.Delete(currentUser.Username);

        set.Update(motorcycle);
    }

    public async Task<Motorcycle?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await GetReadOnlyQueryable().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(int, List<Motorcycle>)> GetListAsync(BaseSpecification<Motorcycle> specification, CancellationToken cancellationToken)
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
