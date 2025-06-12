using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.Publishers;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Car.ToggleActivation;

public class ToggleActivationCarCommandHandler(IUnitOfWork unitOfWork, CarMessagePublisher publisher) : IRequestHandler<ToggleActivationCarCommand>
{
    public async Task Handle(ToggleActivationCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.ToggleActivation();

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);

        var message = car.ToMessage();
        await publisher.PublishMessageAsync(message, cancellationToken);
    }
}