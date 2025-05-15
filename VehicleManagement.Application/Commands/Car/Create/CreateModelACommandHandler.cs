using MediatR;
using Shirin.DomainService.Services;

namespace VehicleManagement.Application.Commands.ModelA.Create;

public class CreateModelACommandHandler(IModelAService service) : IRequestHandler<CreateModelACommand>
{
    public async Task Handle(CreateModelACommand request, CancellationToken cancellationToken)
    {
        await service.CreateAsync(request.Title, cancellationToken);
    }
}
