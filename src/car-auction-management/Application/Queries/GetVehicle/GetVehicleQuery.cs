namespace Application.Queries.GetVehicle;

using Domain.Entities;

using MediatR;

public record GetVehicleQuery : IRequest<Vehicle>
{
    public Guid Id { get; init; }
}
