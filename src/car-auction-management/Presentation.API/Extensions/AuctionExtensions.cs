namespace Presentation.API.Extensions;

using System.ComponentModel;

using Domain.Entities;

using Presentation.API.Controllers.V1.DTOs;

public static class AuctionExtensions
{
    public static Domain.Enums.AuctionStatus ToDomain(this AuctionStatus status)
    {
        return status switch
        {
            AuctionStatus.Inactive => Domain.Enums.AuctionStatus.Inactive,
            AuctionStatus.Active => Domain.Enums.AuctionStatus.Active,
            _ => throw new InvalidEnumArgumentException(
                $"Invalid Auction Status: {status}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(AuctionStatus)))}")
        };
    }

    public static AuctionStatus FromDomain(this Domain.Enums.AuctionStatus status)
    {
        return status switch
        {
            Domain.Enums.AuctionStatus.Inactive => AuctionStatus.Inactive,
            Domain.Enums.AuctionStatus.Active => AuctionStatus.Active,
            _ => throw new InvalidEnumArgumentException(
                $"Invalid Auction Status: {status}. Valid values are: {string.Join(", ", Enum.GetNames(typeof(AuctionStatus)))}")
        };
    }

    public static AuctionDto FromDomain(this Auction auction)
    {
        return new AuctionDto
        {
            Id = auction.Id,
            VehicleId = auction.VehicleId,
            StartingBid = auction.StartingBid,
            Status = auction.Status.FromDomain(),
            EndDate = auction.EndDate,
            StartDate = auction.StartDate
        };
    }
}
