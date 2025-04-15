namespace Domain.Entities;

using Domain.Enums;

public abstract class Vehicle : Entity
{
    public string Model { get; set; }

    public string Manufacturer { get; set; }

    public int Year { get; set; }

    public double StartingBid { get; set; }

    public VehicleType Type { get; set; }
}
