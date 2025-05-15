using MediatR;

namespace VehicleManagement.Application.Commands.ModelA.ToggleActivation;


public record ToggleActivationModelACommand(int Id) : IRequest;
