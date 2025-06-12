using MediatR;
using VehicleManagement.DomainModel.Enums;
using VehicleManagement.DomainService.Data;

namespace VehicleManagement.Application.Events.Car.CreateOrUpdate;

public class CarCreateOrUpdateEventHandler(IReadUnitOfWork unitOfWork) : INotificationHandler<CarCreateOrUpdateEvent>
{
    public async Task Handle(CarCreateOrUpdateEvent notification, CancellationToken cancellationToken)
    {
        var gearbox = notification.Gearbox == "Manual" ? GearboxType.Manual : GearboxType.Automatic;

        var car = await unitOfWork.CarRepository.GetByIdAsync(notification.Id, cancellationToken);
        if (car is null)
        {
            car = DomainModel.Models.CarAggregate.Car.Create(
                notification.Id,
                notification.Title,
                gearbox);

            await unitOfWork.CarRepository.AddAsync(car, cancellationToken);
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

                unitOfWork.CarRepository.Update(car);
            }
            else
            {
                unitOfWork.CarRepository.Delete(car);
            }
        }
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
