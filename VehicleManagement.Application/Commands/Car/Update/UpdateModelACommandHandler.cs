using MediatR;
using Shirin.DomainService.Services;

namespace VehicleManagement.Application.Commands.ModelA.Update;

public class UpdateModelACommandHandler(IModelAService service) : IRequestHandler<UpdateModelACommand>
{
    public async Task Handle(UpdateModelACommand request, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(request.Id, request.Title, cancellationToken);
    }
}
