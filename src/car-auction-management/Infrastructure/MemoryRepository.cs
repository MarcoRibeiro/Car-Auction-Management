﻿namespace Infrastructure;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Domain.Entities;

public abstract class MemoryRepository<T> 
    where T : Entity
{
    private readonly ConcurrentDictionary<Guid, T> _data = new();

    public Task<Guid> CreateAsync(T element)
    {
        _data.TryAdd(element.Id, element);
        return Task.FromResult(element.Id);
    }

    public Task DeleteAsync(Guid id)
    {
        return Task.FromResult(_data.TryRemove(id, out _));
    }

    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
    {
        if(predicate != null)
        {
            return Task.FromResult(_data.Values.Where(predicate.Compile()));
        }

        return Task.FromResult((IEnumerable<T>)_data.Values);
    }

    public Task<T> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_data.TryGetValue(id, out var value) ? value : null);
    }

    public Task UpdateAsync(T element)
    {
        return Task.FromResult(_data.TryUpdate(element.Id, element, _data[element.Id]));
    }
}
