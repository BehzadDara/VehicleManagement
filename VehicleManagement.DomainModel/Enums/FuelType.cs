using System.ComponentModel;
using VehicleManagement.DomainModel.Attributes;

namespace VehicleManagement.DomainModel.Enums;

[EnumEndpoint("/FuelTypes")]
public enum FuelType
{
    [Description("بنزینی")]
    Gas = 1,
    [Description("برقی")]
    Electronic = 2
}
