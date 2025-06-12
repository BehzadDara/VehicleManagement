using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService.Data;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Queries.Motorcycle.GetList;

public class GetMotorcycleListQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GetMotorcycleListQuery, PaginationResult<MotorcycleViewModel>>
{
    public async Task<PaginationResult<MotorcycleViewModel>> Handle(GetMotorcycleListQuery request, CancellationToken cancellationToken)
    {
        var key = $"MotorcycleList:{JsonSerializer.Serialize(request)}";
        var paginationResult = memoryCache.Get<PaginationResult<MotorcycleViewModel>>(key);
        if (paginationResult is null)
        {
            var specification = new GetMotorcyclesByFilterSpecification(request.Q, request.OrderType, request.PageSize, request.PageNumber);
            var (totalCount, cars) = await unitOfWork.MotorcycleRepository.GetListAsync(specification, cancellationToken);

            var viewModels = cars.ToViewModel();
            paginationResult = PaginationResult<MotorcycleViewModel>.Create(request.PageSize ?? 0, request.PageNumber ?? 0, totalCount, viewModels);

            memoryCache.Set(key, paginationResult, TimeSpan.FromSeconds(30));
        }

        return paginationResult;
    }
}
