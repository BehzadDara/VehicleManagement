using VehicleManagement.DomainModel.Models.BackOfficeUserAggregate;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.DomainService.Specifications;

public class GetBackOfficeUserWithUsernameAndPasswordSpecification : BaseSpecification<BackOfficeUser>
{
    public GetBackOfficeUserWithUsernameAndPasswordSpecification(string username, string password)
    {
        AddCriteria(x => x.Username == username);
        AddCriteria(x => x.Password == password);
    }
}
