using MediatR;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.Application.Commands.Car.Create;

public record CreateCarCommand(string Title, GearboxType Gearbox) : IRequest;
