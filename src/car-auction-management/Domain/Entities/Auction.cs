namespace Domain.Entities;

using Domain.Enums;

public class Auction : Entity
{
    public Guid VehicleId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public double StartingBid { get; set; }

    public double? CurrentBid { get; set; }

    public AuctionStatus Status { get; set; }
}
