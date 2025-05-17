using MediatR;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Application.Commands.Motorcycle.Create;

public class CreateMotorcycleCommandHandler(IUnitOfWork unitOfWork, ITrackingCodeProxy trackingCodeProxy) : IRequestHandler<CreateMotorcycleCommand>
{
    public async Task Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var trackingCode = await trackingCodeProxy.Get(cancellationToken);

        var motorcycle = DomainModel.Models.Motorcycle.Create(request.Title, trackingCode, request.Fuel);

        await unitOfWork.MotorcycleRepository.AddAsync(motorcycle, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
