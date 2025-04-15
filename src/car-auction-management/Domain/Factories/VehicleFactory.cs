namespace Domain.Factories;

using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

public class VehicleFactory : IVehicleFactory
{
    public Vehicle Create(VehicleType type, string model, string manufacturer, int year, double startingBid, int numberOfDoors, int loadCapacity, int numberOfSeats)
    {
        return type switch
        {
            VehicleType.Hatchback => new Hatchback
            {
                Id = Guid.NewGuid(),
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfDoors = numberOfDoors,
                Type = type
            },
            VehicleType.Sudan => new Sudan
            {
                Id = Guid.NewGuid(),
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfDoors = numberOfDoors,
                Type = type
            },
            VehicleType.Truck => new Truck
            {
                Id = Guid.NewGuid(),
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                LoadCapacity = loadCapacity,
                Type = type
            },
            VehicleType.SUV => new Suv
            {
                Id = Guid.NewGuid(),
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfSeats = numberOfSeats,
                Type = type
            },
            _ => throw new BusinessRuleException("Vehicle type not supported")
        };
    }
}
