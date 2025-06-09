using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.DeleteOption;

public class DeleteCarOptionCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCarOptionCommand>
{
    public async Task Handle(DeleteCarOptionCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.RemoveOption(request.OptionId);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
