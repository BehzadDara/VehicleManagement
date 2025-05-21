using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.CreateOption;

public class CreateCarOptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCarOptionCommand>
{
    public async Task Handle(CreateCarOptionCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Car), request.Id));

        car.AddOption(request.Description);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
