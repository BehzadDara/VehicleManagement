using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class BackOfficeUserRepository(VehicleManagementDBContext db, ICurrentUser currentUser) : IBackOfficeUserRepository
{

    private readonly DbSet<BackOfficeUser> set = db.Set<BackOfficeUser>();

    private IQueryable<BackOfficeUser> GetQueryable(IQueryable<BackOfficeUser> query)
    {
        if (currentUser.HasGodAccess)
        {
            query = query.IgnoreQueryFilters();
        }

        return query;
    }

    private IQueryable<BackOfficeUser> GetGeneralQueryable()
    {
        var query = set.AsQueryable();

        return GetQueryable(query);
    }

    public async Task<BackOfficeUser?> GetAsync(BaseSpecification<BackOfficeUser> specification, CancellationToken cancellationToken)
    {
        var query = GetGeneralQueryable().Include(x => x.Roles).ThenInclude(x => x.Permissions).Specify(specification);

        var backOfficeUser = await query.FirstOrDefaultAsync(cancellationToken);

        return backOfficeUser;
    }

    public void Update(BackOfficeUser backOfficeUser)
    {
        set.Update(backOfficeUser);
    }
}
