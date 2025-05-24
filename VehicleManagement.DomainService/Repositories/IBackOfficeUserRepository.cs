using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.DomainService.Repositories;

public interface IBackOfficeUserRepository
{
    public Task<BackOfficeUser?> GetAsync(BaseSpecification<BackOfficeUser> specification, CancellationToken cancellationToken);
    public void Update(BackOfficeUser backOfficeUser);
}
