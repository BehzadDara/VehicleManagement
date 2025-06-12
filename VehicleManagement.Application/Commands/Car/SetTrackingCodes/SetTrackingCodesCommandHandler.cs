using MediatR;
using VehicleManagement.DomainService.Data;
using VehicleManagement.DomainService.Resolvers;
using VehicleManagement.DomainService.Specifications;

namespace VehicleManagement.Application.Commands.Car.SetTrackingCodes;

public class SetTrackingCodesCommandHandler(
    IUnitOfWork unitOfWork, 
    ITrackingCodeResolver trackingCodeResolver
    ) : IRequestHandler<SetTrackingCodesCommand>
{
    public async Task Handle(SetTrackingCodesCommand request, CancellationToken cancellationToken)
    {
        var specification = new GetCarsWithNoTrackinCodeSpecification();
        var entities = await unitOfWork.CarRepository.GetListAsync(specification, cancellationToken);

        if (entities.Count == 0)
        {
            return;
        }

        List<string>? trackingCodes;
        try
        {
            // Strategy pattern
            var trackingCodeProxy = trackingCodeResolver.Resolve(entities.Count);
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
