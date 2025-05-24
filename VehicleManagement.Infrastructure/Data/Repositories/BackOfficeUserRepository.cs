using Microsoft.EntityFrameworkCore;
using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainService.BaseSpecifications;
using VehicleManagement.DomainService.Repositories;
using VehicleManagement.Infrastructure.Data.DBContexts;
using VehicleManagement.Infrastructure.Helpers;

namespace VehicleManagement.Infrastructure.Data.Repositories;

public class BackOfficeUserRepository(VehicleManagementDBContext db) : IBackOfficeUserRepository
{
    private readonly DbSet<BackOfficeUser> set = db.Set<BackOfficeUser>();

    public async Task<BackOfficeUser?> GetAsync(BaseSpecification<BackOfficeUser> specification, CancellationToken cancellationToken)
    {
        var query = set.Specify(specification);

        var backOfficeUser = await query.FirstOrDefaultAsync(cancellationToken);

        return backOfficeUser;
    }

    public void Update(BackOfficeUser backOfficeUser)
    {
        set.Update(backOfficeUser);
    }
}
