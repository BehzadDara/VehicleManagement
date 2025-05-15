using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleManagement.API.DTOs;
using VehicleManagement.API.Features;
using VehicleManagement.Application.Commands.Car.Create;
using VehicleManagement.Application.Commands.Car.Delete;
using VehicleManagement.Application.Commands.Car.ToggleActivation;
using VehicleManagement.Application.Commands.Car.Update;
using VehicleManagement.Application.Queries.Car.GetById;
using VehicleManagement.Application.Queries.Car.GetList;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.API.Controllers;

[ApiController]
[Route("Cars")]
public class CarController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateCarDTO input, CancellationToken cancellationToken)
    {
        var command = new CreateCarCommand(input.Title, input.Gearbox);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateOrUpdateCarDTO input, CancellationToken cancellationToken)
    {
        var command = new UpdateCarCommand(id, input.Title, input.Gearbox);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCarCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var query = new GetCarByIdQuery(id);
        var entity = await mediator.Send(query, cancellationToken);

        return Ok(BaseResult.Success(entity));
    }

    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] string? q, 
        [FromQuery] OrderType? orderType,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber,
        CancellationToken cancellationToken)
    {
        var query = new GetCarListQuery(q, orderType, pageSize, pageNumber);
        var entities = await mediator.Send(query, cancellationToken);

        return Ok(BaseResult.Success(entities));
    }

    [HttpPut("{id:int}/ToggleActivation")]
    public async Task<IActionResult> Activate([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivationCarCommand(id);
        await mediator.Send(command, cancellationToken);

        return Ok(BaseResult.Success());
    }
}