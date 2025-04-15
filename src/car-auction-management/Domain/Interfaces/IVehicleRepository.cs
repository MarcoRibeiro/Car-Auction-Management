namespace Domain.Interfaces;

using System.Linq.Expressions;

using Domain.Entities;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> predicate = null);

    Task<Vehicle> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(Vehicle vehicle);

    Task UpdateAsync(Vehicle vehicle);

    Task DeleteAsync(Guid id);
}
