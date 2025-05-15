using MediatR;
using Microsoft.Extensions.Caching.Memory;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Queries.Motorcycle.GetById;

public class GetMotorcycleByIdQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IRequestHandler<GetMotorcycleByIdQuery, MotorcycleViewModel>
{
    public async Task<MotorcycleViewModel> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
    {
        var viewModel = memoryCache.Get<MotorcycleViewModel>(request.Id);
        if (viewModel is null)
        {
            var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.Id));

            viewModel = motorcycle.ToViewModel();

            memoryCache.Set(request.Id, viewModel, TimeSpan.FromSeconds(30));
        }

        return viewModel;
    }
}
