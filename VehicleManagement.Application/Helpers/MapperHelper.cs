using AutoMapper;
using VehicleManagement.Application.Publishers;
using VehicleManagement.Application.ViewModels;
using VehicleManagement.DomainModel.Models.CarAggregate;
using VehicleManagement.DomainModel.Models.MotorcycleAggregate;

namespace VehicleManagement.Application.Helpers;

public static class MapperHelper
{
    public static CarViewModel ToViewModel(this Car entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Car, CarViewModel>()
            .ForMember(dest => dest.Gearbox, opt => opt.MapFrom(src => src.Gearbox.ToString()));
        });

        var mapper = new Mapper(config);

        return mapper.Map<CarViewModel>(entity);
    }

    public static List<CarViewModel> ToViewModel(this List<Car> entities)
    {
        return entities.Select(x => new CarViewModel
        {
            Id = x.Id,
            Title = x.Title,
            TrackingCode = x.TrackingCode,
            Gearbox = x.Gearbox.ToString(),
        }).ToList();
    }

    public static CarMessage ToMessage(this Car entity)
    {
        return  new CarMessage
        {
            Id = entity.Id,
            Title = entity.Title,
            Gearbox = entity.Gearbox.ToString(),
            IsActive = entity.IsActive,
            IsDeleted = entity.IsDeleted,
        };
    }

    public static MotorcycleViewModel ToViewModel(this Motorcycle entity)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Motorcycle, MotorcycleViewModel>()
            .ForMember(dest => dest.Fuel, opt => opt.MapFrom(src => src.Fuel.ToString()));
        });

        var mapper = new Mapper(config);

        return mapper.Map<MotorcycleViewModel>(entity);
    }

    public static List<MotorcycleViewModel> ToViewModel(this List<Motorcycle> entities)
    {
        return entities.Select(x => new MotorcycleViewModel
        {
            Id = x.Id,
            Title = x.Title,
            TrackingCode = x.TrackingCode,
            Fuel = x.Fuel.ToString(),
        }).ToList();
    }
}
