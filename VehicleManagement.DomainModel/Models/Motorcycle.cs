﻿using VehicleManagement.DomainModel.BaseModels;
using VehicleManagement.DomainModel.Enums;

namespace VehicleManagement.DomainModel.Models;

public class Motorcycle : TrackableEntity
{
    public string Title { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string TrackingCode { get; set; } = string.Empty;
    public FuelType Fuel {  get; set; }

    private Motorcycle(string title, FuelType fuel)
    {
        Title = title;
        IsActive = true;
        Fuel = fuel;
    }

    public static Motorcycle Create(string title, FuelType fuel)
    {
        return new Motorcycle(title, fuel);
    }

    public void Update(string title, FuelType fuel)
    {
        Title = title;
        Fuel = fuel;
    }

    public void ToggleActivation()
    {
        IsActive = !IsActive;
    }
}
