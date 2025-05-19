using MediatR;
using VehicleManagement.Application.Commands.Car.SetTrackingCodes;

namespace VehicleManagement.Application.Jobs;

public class CarsTrackingCodeJob(IMediator mediator)
{
    public async Task Get()
    {
        var command = new SetTrackingCodesCommand();
        await mediator.Send(command);
    }
}
