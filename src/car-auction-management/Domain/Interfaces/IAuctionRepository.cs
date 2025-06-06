﻿namespace Domain.Interfaces;

using System.Linq.Expressions;

using Domain.Entities;

public interface IAuctionRepository
{
    Task<IEnumerable<Auction>> GetAllAsync(Expression<Func<Auction, bool>> predicate = null);

    Task<Auction> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(Auction auction);

    Task UpdateAsync(Auction auction);

    Task DeleteAsync(Guid id);
}
