namespace Presentation.API.Controllers.V1.Extensions;

using System.ComponentModel;

using Presentation.API.Controllers.V1.DTOs;

public static class VehicleTypeExtensions
{
    public static Domain.Enums.VehicleType ToDomain(this VehicleType type)
    {
        return type switch
        {
            VehicleType.Truck => Domain.Enums.VehicleType.Truck,
            VehicleType.Hatchback => Domain.Enums.VehicleType.Hatchback,
            VehicleType.SUV => Domain.Enums.VehicleType.SUV,
            VehicleType.Sudan => Domain.Enums.VehicleType.Sudan,
            _ => throw new InvalidEnumArgumentException(
                $"Invalid vehicle type: {type}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(VehicleType)))}")
        };
    }
}
