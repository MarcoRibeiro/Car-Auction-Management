namespace Presentation.API.Extensions;

using System.ComponentModel;

using Domain.Entities;

using Presentation.API.Controllers.V1.DTOs;

public static class VehicleExtensions
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

    public static VehicleType FromDomain(this Domain.Enums.VehicleType type)
    {
        return type switch
        {
            Domain.Enums.VehicleType.Truck => VehicleType.Truck,
            Domain.Enums.VehicleType.Hatchback => VehicleType.Hatchback,
            Domain.Enums.VehicleType.SUV => VehicleType.SUV,
            Domain.Enums.VehicleType.Sudan => VehicleType.Sudan,
            _ => throw new InvalidEnumArgumentException(
                $"Invalid vehicle type: {type}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(VehicleType)))}")
        };
    }

    public static VehicleDto ToDto(this Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            Type = vehicle.Type.FromDomain(),
            Manufacturer = vehicle.Manufacturer,
            Model = vehicle.Model,
            Year = vehicle.Year,
            StartingBid = vehicle.StartingBid,
            NumberOfDoors = vehicle.GetNumberOfDoors(),
            NumberOfSeats = vehicle is Suv suv ? suv.NumberOfSeats : null,
            LoadCapacity = vehicle is Truck truck ? truck.LoadCapacity : null,
        };
    }

    private static int? GetNumberOfDoors(this Vehicle vehicle)
    {
        return vehicle switch
        {
            Hatchback hatchback => hatchback.NumberOfDoors,
            Sudan sudan => sudan.NumberOfDoors,
            _ => null
        };
    }
}
