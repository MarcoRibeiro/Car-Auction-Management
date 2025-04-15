namespace Application.Commands.PlaceBid;

using MediatR;

public record StopAuctionCommand : IRequest
{
    public Guid VehicleId { get; init; }
    
    public Guid UserId { get; init; }

    public double Amount { get; init; }
}
