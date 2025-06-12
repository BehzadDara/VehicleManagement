using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.Publishers;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Car.Update;

public class UpdateCarCommandHandler(IUnitOfWork unitOfWork, CarMessagePublisher publisher) : IRequestHandler<UpdateCarCommand>
{
    public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.Update(request.Title, request.Gearbox);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);

        var message = car.ToMessage();
        await publisher.PublishMessageAsync(message, cancellationToken);
    }
}
