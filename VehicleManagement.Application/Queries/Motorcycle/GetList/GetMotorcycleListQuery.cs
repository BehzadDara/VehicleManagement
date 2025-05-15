using MediatR;
using VehicleManagement.Application.Features;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.Application.Queries.Motorcycle.GetList;

public record GetMotorcycleListQuery(
    string? Q,
    OrderType? OrderType,
    int? PageSize,
    int? PageNumber
    ) : IRequest<PaginationResult<MotorcycleViewModel>>;