using MediatR;
using Shirin.DomainService.Services;

namespace VehicleManagement.Application.Commands.ModelA.ToggleActivation;

public class ToggleActivationModelACommandHandler(IModelAService service) : IRequestHandler<ToggleActivationModelACommand>
{
    public async Task Handle(ToggleActivationModelACommand request, CancellationToken cancellationToken)
    {
        await service.ToggleActivationAsync(request.Id, cancellationToken);
    }
}