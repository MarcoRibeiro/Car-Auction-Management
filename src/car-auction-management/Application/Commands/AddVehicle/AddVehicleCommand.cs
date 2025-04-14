namespace Application.Commands.AddVehicle;

using Domain.Enums;
using MediatR;

public record AddVehicleCommand : IRequest<Guid>
{
    public string Model { get; init; }

    public string Manufacturer { get; init; }

    public int Year { get; init; }

    public double Startingid { get; init; }

    public VehicleType Type { get; init; }

    public int NumberOfDoors { get; init; }

    public int LoadCapacity { get; init; }

    public int NumberOfSeats { get; init; }
}
