namespace Application.Commands.StartAuction;

using MediatR;

public record StartAuctionCommand : IRequest<Guid>
{
    public Guid VehicleId { get; init; }
}
