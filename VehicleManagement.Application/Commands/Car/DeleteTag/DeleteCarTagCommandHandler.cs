using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Car.DeleteTag;

public class DeleteCarTagCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCarTagCommand>
{
    public async Task Handle(DeleteCarTagCommand request, CancellationToken cancellationToken)
    {
        var car = await unitOfWork.CarRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.CarAggregate.Car), request.Id));

        car.RemoveTag(request.Title, request.Priority);

        unitOfWork.CarRepository.Update(car);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
