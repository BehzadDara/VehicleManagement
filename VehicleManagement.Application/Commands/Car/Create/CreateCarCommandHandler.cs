using MediatR;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;

namespace VehicleManagement.Application.Commands.Car.Create;

public class CreateCarCommandHandler(IUnitOfWork unitOfWork, ITrackingCodeProxy trackingCodeProxy) : IRequestHandler<CreateCarCommand>
{
    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var trackingCode = await trackingCodeProxy.Get(cancellationToken);

        var car = DomainModel.Models.Car.Create(request.Title, trackingCode, request.Gearbox);

        await unitOfWork.CarRepository.AddAsync(car, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
