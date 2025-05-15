using MediatR;
using Shirin.DomainService.Services;

namespace VehicleManagement.Application.Commands.ModelA.Delete;

public class DeleteModelACommandHandler(IModelAService service) : IRequestHandler<DeleteModelACommand>
{
    public async Task Handle(DeleteModelACommand request, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(request.Id, cancellationToken);
    }
}