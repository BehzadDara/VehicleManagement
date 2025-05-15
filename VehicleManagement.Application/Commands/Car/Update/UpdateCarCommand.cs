using MediatR;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.Application.Commands.Car.Update;

public record UpdateCarCommand(int Id, string Title, GearboxType Gearbox) : IRequest;