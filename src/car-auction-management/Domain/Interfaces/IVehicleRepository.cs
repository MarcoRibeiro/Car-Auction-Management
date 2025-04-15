namespace Domain.Interfaces;

using Domain.Entities;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllCarsAsync();

    Task<Vehicle> GetVehicleByIdAsync(Guid id);

    Task<Guid> CreateVehicleAsync(Vehicle vehicle);

    Task UpdateCarAsync(Vehicle vehicle);

    Task DeleteCarAsync(Guid id);
}
