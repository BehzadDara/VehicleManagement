using MediatR;

namespace VehicleManagement.Application.Commands.Motorcycle.Delete;

public record DeleteMotorcycleCommand(int Id) : IRequest;
