using MediatR;

namespace VehicleManagement.Application.Commands.Car.ToggleActivation;


public record ToggleActivationCarCommand(int Id) : IRequest;
