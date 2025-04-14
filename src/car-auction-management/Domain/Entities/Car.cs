namespace Domain.Entities;

using System;
using Domain.Enums;

public abstract class Car
{
    public Guid Id { get; set; }

    public string Make { get; set; }

    public string Manufacturer { get; set; }

    public int Year { get; set; }

    public double Startingid { get; set; }

    public CarType Type { get; set; }
}
