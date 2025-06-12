using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Car.CreateTag;

public class CreateCarTagCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCarTagCommand>
{
    public async Task Handle(CreateCarTagCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.AddTag(request.Title, request.Priority);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
