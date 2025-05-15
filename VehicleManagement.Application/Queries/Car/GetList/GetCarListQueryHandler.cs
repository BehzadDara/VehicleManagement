using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainModel.Models;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Queries.Car.GetList;

public class GetCarListQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GetCarListQuery, PaginationResult<CarViewModel>>
{
    public async Task<PaginationResult<CarViewModel>> Handle(GetCarListQuery request, CancellationToken cancellationToken)
    {
        var key = $"CarList:{JsonSerializer.Serialize(request)}";
        var paginationResult = memoryCache.Get<PaginationResult<CarViewModel>>(key);
        if (paginationResult is null)
        {
            var specification = new GetCarsByFilterSpecification(request.Q, request.OrderType, request.PageSize, request.PageNumber);
            var (totalCount, cars) = await unitOfWork.CarRepository.GetListAsync(specification, cancellationToken);

            var viewModels = cars.ToViewModel();
            paginationResult = PaginationResult<CarViewModel>.Create(request.PageSize ?? 0, request.PageNumber ?? 0, totalCount, viewModels);

            memoryCache.Set(key, paginationResult, TimeSpan.FromSeconds(30));
        }

        return paginationResult;
    }
}
