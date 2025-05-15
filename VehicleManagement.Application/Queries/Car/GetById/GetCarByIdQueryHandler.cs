using MediatR;
using Microsoft.Extensions.Caching.Memory;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Queries.Car.GetById;

public class GetCarByIdQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GetCarByIdQuery, CarViewModel>
{
    public async Task<CarViewModel> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var viewModel = memoryCache.Get<CarViewModel>(request.Id);
        if (viewModel is null)
        {
            var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Car), request.Id));

            viewModel = car.ToViewModel();

            memoryCache.Set(request.Id, viewModel, TimeSpan.FromSeconds(30));
        }

        return viewModel;
    }
}
