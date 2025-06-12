using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Car.CreateOption;

public class CreateCarOptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCarOptionCommand>
{
    public async Task Handle(CreateCarOptionCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.AddOption(request.Description);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
