using MediatR;
using VehicleManagement.Application.Exceptions;
using VehicleManagement.DomainService;
using VehicleManagement.Resources;

namespace VehicleManagement.Application.Commands.Motorcycle.ToggleActivation;

public class ToggleActivationMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivationMotorcycleCommand>
{
    public async Task Handle(ToggleActivationMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = await unitOfWork.MotorcycleRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.Motorcycle), request.Id));

        motorcycle.ToggleActivation();

        unitOfWork.MotorcycleRepository.Update(motorcycle);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}