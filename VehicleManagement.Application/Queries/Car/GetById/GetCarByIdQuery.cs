using MediatR;
using VehicleManagement.Application.ViewModels;

namespace VehicleManagement.Application.Queries.Car.GetById;

public record GetCarByIdQuery(int Id) : IRequest<CarViewModel>;
