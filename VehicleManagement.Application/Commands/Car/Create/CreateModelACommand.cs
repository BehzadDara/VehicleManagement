using MediatR;

namespace VehicleManagement.Application.Commands.ModelA.Create;

public record CreateModelACommand(string Title) : IRequest;
