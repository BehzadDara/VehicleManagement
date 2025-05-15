using MediatR;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.Application.Commands.Motorcycle.Create;

public record CreateMotorcycleCommand(string Title, FuelType Fuel) : IRequest;
