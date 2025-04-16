namespace Presentation.API.Extensions;

using System.ComponentModel;

using Presentation.API.Controllers.V1.DTOs;

public static class AuctionStatusExtensions
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
}
