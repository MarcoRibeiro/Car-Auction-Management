namespace Application.Queries.SearchVehicles;

using Domain.Entities;
using Domain.Enums;

using MediatR;

public record SearchVehiclesQuery : IRequest<IEnumerable<Vehicle>>
{
    public string Model { get; init; }

    public string Manufacturer { get; init; }

    public int? Year { get; init; }

    public VehicleType? Type { get; init; }
}
