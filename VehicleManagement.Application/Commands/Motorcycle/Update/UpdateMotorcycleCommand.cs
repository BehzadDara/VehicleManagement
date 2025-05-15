using MediatR;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.Application.Commands.Motorcycle.Update;

public record UpdateMotorcycleCommand(int Id, string Title, FuelType Fuel) : IRequest;