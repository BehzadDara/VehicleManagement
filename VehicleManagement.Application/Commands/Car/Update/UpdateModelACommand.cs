using MediatR;

namespace VehicleManagement.Application.Commands.ModelA.Update;

public record UpdateModelACommand(int Id, string Title) : IRequest;