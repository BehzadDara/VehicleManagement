using MediatR;
using VehicleManagement.Application.Helpers;
using VehicleManagement.Application.Publishers;
using VehicleManagement.DomainService;

namespace VehicleManagement.Application.Commands.Car.Create;

public class CreateCarCommandHandler(IUnitOfWork unitOfWork, CarMessagePublisher publisher) : IRequestHandler<CreateCarCommand>
{
    public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = DomainModel.Models.CarAggregate.Car.Create(request.Title, request.Gearbox);

        await unitOfWork.CarRepository.AddAsync(car, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);

        var message = car.ToMessage();
        await publisher.PublishMessageAsync(message, cancellationToken);
    }
}
