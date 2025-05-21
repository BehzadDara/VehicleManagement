using MediatR;

namespace VehicleManagement.Application.Commands.Car.DeleteTag;

public record DeleteCarTagCommand(int Id, string Title, int Priority) : IRequest;
