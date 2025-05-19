using MediatR;
using VehicleManagement.DomainService;
using VehicleManagement.DomainService.Proxies;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Commands.Car.SetTrackingCodes;

public class SetTrackingCodesCommandHandler(
    IUnitOfWork unitOfWork, 
    ITrackingCodeProxy trackingCodeProxy
    ) : IRequestHandler<SetTrackingCodesCommand>
{
    public async Task Handle(SetTrackingCodesCommand request, CancellationToken cancellationToken)
    {
        var specification = new GetCarsWithNoTrackinCodeSpecification();
        var (_, entities) = await unitOfWork.CarRepository.GetListAsync(specification, cancellationToken);

        if (entities.Count == 0)
        {
            return;
        }

        List<string>? trackingCodes;
        try
        {
            trackingCodes = await trackingCodeProxy.Get(entities.Count, cancellationToken);

            if (trackingCodes.Count != entities.Count)
            {
                // Log Count TrackingCode
                return;
            }
        }
        catch
        {
            // Log Error TrackingCode
            return;
        }

        for ( var i = 0; i < entities.Count; i++ )
        {
            entities[i].SetTrackingCode(trackingCodes[i]); 
        }

        try
        {
            unitOfWork.CarRepository.Update(entities);
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            // Log Error Repeat TrackingCode
        }
    }
}
