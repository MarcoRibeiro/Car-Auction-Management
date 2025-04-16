namespace Presentation.API.Controllers.V1.DTOs;

public record VehicleDto
{
    public Guid Id { get; init; }

    public string Model { get; init; }

    public string Manufacturer { get; init; }

    public int Year { get; init; }

    public double StartingBid { get; init; }

    public int? NumberOfDoors { get; init; }

    public int? LoadCapacity { get; init; }

    public int? NumberOfSeats { get; init; }

    public VehicleType Type { get; init; }
}
