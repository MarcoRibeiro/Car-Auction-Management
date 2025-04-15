namespace Infrastructure;

using Domain.Entities;
using Domain.Interfaces;

public class AuctionRepository : MemoryRepository<Auction>, IAuctionRepository
{
}
