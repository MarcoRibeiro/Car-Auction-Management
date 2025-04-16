namespace Application.Queries.SearchAuctions;

using Application.Extensions;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

public record SearchVehiclesQueryHandler(IAuctionRepository auctionRepository) :
    IRequestHandler<SearchAuctionsQuery, IEnumerable<Auction>>
{
    public async Task<IEnumerable<Auction>> Handle(SearchAuctionsQuery request, CancellationToken cancellationToken)
    {
        return await auctionRepository.GetAllAsync(request.ToExpression());
    }
}