namespace Infrastructure;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Entities;

public class MemoryRepository<T> 
    where T : Entity
{
    private readonly ConcurrentDictionary<Guid, T> data = new();

    public Task<Guid> CreateAsync(T element)
    {
        this.data.TryAdd(element.Id, element);
        return Task.FromResult(element.Id);
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate = null)
    {
        if(predicate != null)
        {
            return Task.FromResult(this.data.Values.Where(predicate));
        }

        return Task.FromResult((IEnumerable<T>)this.data.Values);
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        return Task.FromResult(this.data.TryGetValue(id, out var value) ? value : null);
    }

    public Task UpdateAsync(T vehicle)
    {
        throw new NotImplementedException();
    }
}
