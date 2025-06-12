using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Motorcycle.Delete;

public class DeleteMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteMotorcycleCommand>
{
    public async Task Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Resources.Messages.NotFound, nameof(DomainModel.Models.MotorcycleAggregate.Motorcycle), request.Id));

        unitOfWork.MotorcycleRepository.Delete(motorcycle);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}