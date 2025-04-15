namespace Application.Commands.StopAuction;

using MediatR;

public record StopAuctionCommand : IRequest
{
    public Guid VehicleId { get; init; }
}
