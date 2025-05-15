using MediatR;

namespace VehicleManagement.Application.Commands.Motorcycle.ToggleActivation;


public record ToggleActivationMotorcycleCommand(int Id) : IRequest;
