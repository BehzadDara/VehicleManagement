using MediatR;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainService;

namespace VehicleManagement.Application.Events.Car.CreateOrUpdate;

public class CarCreateOrUpdateEventHandler(IUnitOfWork unitOfWork) : INotificationHandler<CarCreateOrUpdateEvent>
{
    public async Task Handle(CarCreateOrUpdateEvent notification, CancellationToken cancellationToken)
    {
        var gearbox = notification.Gearbox == "Manual" ? GearboxType.Manual : GearboxType.Automatic;

        var car = await unitOfWork.CarReadRepository.GetByIdAsync(notification.Id, cancellationToken);
        if (car is null)
        {
            car = DomainModel.Models.CarAggregate.Car.Create(
                notification.Id,
                notification.Title,
                gearbox);

            await unitOfWork.CarReadRepository.AddAsync(car, cancellationToken);
        }
        else
        {
            if (!notification.IsDeleted)
            {
                car.Update(
                    notification.Title,
                    gearbox,
                    notification.IsActive
                    );

                unitOfWork.CarReadRepository.Update(car);
            }
            else
            {
                unitOfWork.CarReadRepository.Delete(car);
            }
        }
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
