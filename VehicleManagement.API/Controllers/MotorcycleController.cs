using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.API.DTOs;
using VehicleManagement.API.Features;
using VehicleManagement.Application.Commands.Motorcycle.Create;
using VehicleManagement.Application.Commands.Motorcycle.Delete;
using VehicleManagement.Application.Commands.Motorcycle.ToggleActivation;
using VehicleManagement.Application.Commands.Motorcycle.Update;
using VehicleManagement.Application.Queries.Motorcycle.GetById;
using VehicleManagement.Application.Queries.Motorcycle.GetList;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.API.Controllers;

[ApiController]
[Route("Motorcycles")]
[Authorize(Policy = "MotorcycleModifyPolicy")]
public class MotorcycleController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateMotorcycleDTO input, CancellationToken cancellationToken)
    {
        var command = new CreateMotorcycleCommand(input.Title, input.Fuel);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateOrUpdateMotorcycleDTO input, CancellationToken cancellationToken)
    {
        var command = new UpdateMotorcycleCommand(id, input.Title, input.Fuel);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteMotorcycleCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetMotorcycleByIdQuery(id);
        var entity = await mediator.Send(query, cancellationToken);

        return Ok(BaseResult.Success(entity));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetList(
        [FromQuery] string? q,
        [FromQuery] OrderType? orderType,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber,
        CancellationToken cancellationToken)
    {
        var query = new GetMotorcycleListQuery(q, orderType, pageSize, pageNumber);
        var entities = await mediator.Send(query, cancellationToken);

        return Ok(BaseResult.Success(entities));
    }

    [HttpPut("{id:int}/ToggleActivation")]
    public async Task<IActionResult> Activate([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivationMotorcycleCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }
}