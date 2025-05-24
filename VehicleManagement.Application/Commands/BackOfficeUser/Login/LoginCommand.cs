using MediatR;

namespace VehicleManagement.Application.Commands.BackOfficeUser.Login;

public record LoginCommand(string Username, string Password) : IRequest<string>;