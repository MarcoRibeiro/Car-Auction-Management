namespace Domain.Interfaces;

using Domain.Entities;
using Domain.Enums;

public interface IVehicleFactory
{
    Vehicle CreateVehicle(
        VehicleType type,
        string model,
        string manufacturer,
        int year,
        double startingPrice,
        int numberOfDoors,
        int loadCapacity,
        int numberOfSeats);
}
