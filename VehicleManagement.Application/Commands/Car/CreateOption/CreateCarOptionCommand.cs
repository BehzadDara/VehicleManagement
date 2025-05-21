using MediatR;

namespace VehicleManagement.Application.Commands.Car.CreateOption;

public record CreateCarOptionCommand(int Id, string Description) : IRequest;
