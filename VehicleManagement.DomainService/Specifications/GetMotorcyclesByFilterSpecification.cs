using VehicleManagement.DomainModel.Models.MotorcycleAggregate;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.DomainService.Specifications;

public class GetMotorcyclesByFilterSpecification : BaseSpecification<Motorcycle>
{
    public GetMotorcyclesByFilterSpecification(
        string? q,
        OrderType? orderType,
        int? pageSize,
        int? pageNumber)
    {
        AddCriteria(x => x.IsActive);

        if (!string.IsNullOrEmpty(q))
        {
            AddCriteria(x => x.Title.Contains(q) || x.TrackingCode.Contains(q));
        }

        if (orderType != null)
        {
            AddOrderBy(x => x.Id, orderType.Value);
        }

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            AddPagination(pageSize.Value, pageNumber.Value);
        }

    }
}
