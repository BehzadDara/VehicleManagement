using MediatR;

namespace VehicleManagement.Application.Commands.Car.Delete;

public record DeleteCarCommand(int Id) : IRequest;
