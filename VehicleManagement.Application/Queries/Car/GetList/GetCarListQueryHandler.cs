using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Queries.Car.GetList;

public class GetCarListQueryHandler(IUnitOfWork unitOfWork, IDistributedCache cache) : IRequestHandler<GetCarListQuery, PaginationResult<CarViewModel>>
{
    public async Task<PaginationResult<CarViewModel>> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"CarList:{JsonSerializer.Serialize(request)}";

        var cacheData = await cache.GetAsync(cacheKey, cancellationToken);
        if (cacheData is not null)
        {
            var result = JsonSerializer.Deserialize<PaginationResult<CarViewModel>>(cacheData)!;
            return result;
        }


        var specification = new GetCarsByFilterSpecification(request.Q, request.OrderType, request.PageSize, request.PageNumber);
        var (totalCount, cars) = await unitOfWork.CarReadRepository.GetListAsync(specification, cancellationToken);

        var viewModels = cars.ToViewModel();
        var paginationResult = PaginationResult<CarViewModel>.Create(request.PageSize ?? 0, request.PageNumber ?? 0, totalCount, viewModels);
        
        cacheData = JsonSerializer.SerializeToUtf8Bytes(paginationResult);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        };
        await cache.SetAsync(cacheKey, cacheData, options, cancellationToken);
        
        return paginationResult;
    }
}
