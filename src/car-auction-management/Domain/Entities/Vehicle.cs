namespace Domain.Entities;

using System;
using Domain.Enums;

public abstract class Vehicle
{
    public Guid Id { get; set; }

    public string Model { get; set; }

    public string Manufacturer { get; set; }

    public int Year { get; set; }

    public double Startingid { get; set; }

    public VehicleType Type { get; set; }
}
