using System.ComponentModel;
using VehicleManagement.DomainModel.Attributes;

namespace VehicleManagement.DomainModel.Enums;

[EnumEndpoint("/GearboxTypes")]
public enum GearboxType
{
    [Description("دنده ای")]
    Manual = 1,
    [Description("اتوماتیک")]
    Automatic = 2
}
