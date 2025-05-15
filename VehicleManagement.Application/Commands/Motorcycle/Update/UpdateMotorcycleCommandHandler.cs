using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Motorcycle.Update;

public class UpdateMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateMotorcycleCommand>
{
    public async Task Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.Id));

        motorcycle.Update(request.Title, request.Fuel);

        unitOfWork.MotorcycleRepository.Update(motorcycle);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
