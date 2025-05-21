using MediatR;

namespace VehicleManagement.Application.Commands.Car.DeleteOption;

public record DeleteCarOptionCommand(int Id, Guid OptionId) : IRequest;