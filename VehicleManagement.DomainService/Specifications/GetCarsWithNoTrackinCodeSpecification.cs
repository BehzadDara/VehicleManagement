using VehicleManagement.DomainModel.Models;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.DomainService.Specifications;

public class GetCarsWithNoTrackinCodeSpecification : BaseSpecification<Car>
{
    public GetCarsWithNoTrackinCodeSpecification()
    {
        AddCriteria(x => string.IsNullOrEmpty(x.TrackingCode));
    }
}
