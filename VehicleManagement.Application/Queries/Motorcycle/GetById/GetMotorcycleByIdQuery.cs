using MediatR;
using VehicleManagement.Application.ViewModels;

namespace VehicleManagement.Application.Queries.Motorcycle.GetById;

public record GetMotorcycleByIdQuery(int Id) : IRequest<MotorcycleViewModel>;
