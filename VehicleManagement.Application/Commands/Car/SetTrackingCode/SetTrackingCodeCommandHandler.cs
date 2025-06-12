using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService.Data;
using VehicleManagement.DomainService.Failovers;

namespace VehicleManagement.Application.Commands.Car.SetTrackingCode;

public class SetTrackingCodeCommandHandler(
    IUnitOfWork unitOfWork, 
    FallbackTrackingCodeProxy fallbackTrackingCodeProxy
    ) : IRequestHandler<SetTrackingCodeCommand>
{
    // Failover pattern
    public async Task Handle(SetTrackingCodeCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        if (string.IsNullOrEmpty(car.TrackingCode))
        {
            var trackingCodes = await fallbackTrackingCodeProxy.Get(1, cancellationToken);
            car.SetTrackingCode(trackingCodes[0]);

            unitOfWork.CarRepository.Update(car);
            await unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
