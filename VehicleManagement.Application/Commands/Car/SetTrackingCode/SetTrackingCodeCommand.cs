using MediatR;

namespace VehicleManagement.Application.Commands.Car.SetTrackingCode;

public record SetTrackingCodeCommand(int Id) : IRequest;
