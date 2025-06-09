using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.Publishers;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.Delete;

public class DeleteCarCommandHandler(IUnitOfWork unitOfWork, CarMessagePublisher publisher) : IRequestHandler<DeleteCarCommand>
{
    public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        unitOfWork.CarRepository.Delete(car);
        await unitOfWork.CommitAsync(cancellationToken);

        var message = car.ToMessage();
        await publisher.PublishMessageAsync(message, cancellationToken);
    }
}