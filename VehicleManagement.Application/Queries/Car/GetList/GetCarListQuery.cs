using MediatR;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.Application.Queries.Car.GetList;

public record GetCarListQuery(
    string? Q,
    OrderType? OrderType,
    int? PageSize,
    int? PageNumber
    ) : IRequest<PaginationResult<CarViewModel>>;