namespace Application.Queries.GetVehicle;

using Domain.Entities;

using MediatR;

public record SearchVehiclesQuery : IRequest<Vehicle>
{
    public Guid Id { get; init; }
}
