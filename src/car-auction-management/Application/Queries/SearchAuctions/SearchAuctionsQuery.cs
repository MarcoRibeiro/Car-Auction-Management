namespace Application.Queries.SearchAuctions;

using Domain.Entities;
using Domain.Enums;

using MediatR;

public record SearchAuctionsQuery : IRequest<IEnumerable<Auction>>
{
    public Guid? VehicleId { get; init; }

    public AuctionStatus? Status { get; init; }
}
