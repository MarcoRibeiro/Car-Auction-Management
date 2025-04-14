namespace Domain.Factories;

using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

public class VehicleFactory : IVehicleFactory
{
    public Vehicle CreateVehicle(VehicleType type, string model, string manufacturer, int year, double startingBid, int numberOfDoors, int loadCapacity, int numberOfSeats)
    {
        return type switch
        {
            VehicleType.Hatchback => new Hatchback
            {
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfDoors = numberOfDoors,
                Type = type
            },
            VehicleType.Sudan => new Sudan
            {
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfDoors = numberOfDoors,
                Type = type
            },
            VehicleType.Truck => new Truck
            {
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                LoadCapacity = loadCapacity,
                Type = type
            },
            VehicleType.SUV => new Suv
            {
                Model = model,
                Manufacturer = manufacturer,
                Year = year,
                StartingBid = startingBid,
                NumberOfSeats = numberOfSeats,
                Type = type
            },
            _ => throw new ArgumentException("Invalid vehicle type")
        };
    }
}
