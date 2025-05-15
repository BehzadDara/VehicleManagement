using System.ComponentModel;
using VehicleManagement.DomainModel.Attributes;

namespace VehicleManagement.DomainModel.Enums;

[EnumEndpoint("/VehicleTypes")]
public enum VehicleType
{
    [Info("Priority", 1)]
    [Info("HasActiveSales", true)]
    [Description("اتومبیل")]
    Car = 1,

    [Info("Priority", 2)]
    [Info("HasActiveSales", true)]
    [Description("موتورسیکلت")]
    Motorcycle = 2
}

