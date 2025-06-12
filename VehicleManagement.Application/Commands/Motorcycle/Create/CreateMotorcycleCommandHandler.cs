using MediatR;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Commands.Motorcycle.Create;

public class CreateMotorcycleCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMotorcycleCommand>
{
    public async Task Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
    {
        var motorcycle = DomainModel.Models.MotorcycleAggregate.Motorcycle.Create(request.Title, request.Fuel);

        await unitOfWork.MotorcycleRepository.AddAsync(motorcycle, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
