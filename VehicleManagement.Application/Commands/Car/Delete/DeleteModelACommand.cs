using MediatR;

namespace VehicleManagement.Application.Commands.ModelA.Delete;

public record DeleteModelACommand(int Id) : IRequest;
