using MediatR;

namespace VehicleManagement.Application.Commands.Car.CreateTag;

public record CreateCarTagCommand(int Id, string Title, int Priority) : IRequest;
