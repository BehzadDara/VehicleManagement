using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Queries.Car.GetById;

public class GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IDistributedCache cache) : IRequestHandler<GetCarByIdQuery, CarViewModel>
{
    public async Task<CarViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"CarById:{request.Id}";

        var cacheData = await cache.GetAsync(cacheKey, cancellationToken);
        if (cacheData is not null)
        {
            var result = JsonSerializer.Deserialize<CarViewModel>(cacheData)!;
            return result;
        }

        var car = await unitOfWork.CarReadRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        var viewModel = car.ToViewModel();

        cacheData = JsonSerializer.SerializeToUtf8Bytes(viewModel);
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        };

        await cache.SetAsync(cacheKey, cacheData, options, cancellationToken);

        return viewModel;
    }
}
