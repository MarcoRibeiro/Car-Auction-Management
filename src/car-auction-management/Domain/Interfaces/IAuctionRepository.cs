namespace Domain.Interfaces;

using Domain.Entities;

public interface IAuctionRepository
{
    Task<IEnumerable<Auction>> GetAllAsync(Func<Auction, bool> predicate = null);

    Task<Auction> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(Auction vehicle);

    Task UpdateAsync(Auction vehicle);

    Task DeleteAsync(Guid id);
}
