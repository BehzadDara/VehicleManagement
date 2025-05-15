using System.ComponentModel;
using VehicleManagement.DomainModel.Attributes;

namespace VehicleManagement.DomainModel.Enums;

[EnumEndpoint("/VehicleTypes")]
public enum VehicleType
{
    [Description("اتومبیل")]
    Car = 1,
    [Description("موتورسیکلت")]
    Motorcycle = 2
}

