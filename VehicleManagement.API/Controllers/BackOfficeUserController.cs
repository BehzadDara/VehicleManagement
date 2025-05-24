using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.API.DTOs;
using VehicleManagement.API.Features;
using VehicleManagement.Application.Commands.BackOfficeUser.Login;

namespace VehicleManagement.API.Controllers;

[ApiController]
[Route("BackOfficeUsers")]
public class BackOfficeUserController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO input, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(input.Username, input.Password);
        var token = await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success(token));
    }
}
