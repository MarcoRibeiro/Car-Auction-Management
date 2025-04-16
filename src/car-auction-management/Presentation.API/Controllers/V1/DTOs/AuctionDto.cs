namespace Presentation.API.Controllers.V1.DTOs;

public record AuctionDto
{
    public Guid Id { get; init; }

    public Guid VehicleId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public double StartingBid { get; init; }

    public double? CurrentBid { get; init; }

    public AuctionStatus Status { get; init; }
}
