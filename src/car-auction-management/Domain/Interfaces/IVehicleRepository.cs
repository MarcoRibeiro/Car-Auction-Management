namespace Domain.Interfaces;

using Domain.Entities;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllCarsAsync();

    Task<Vehicle> GetVehicleByIdAsync(Guid id);

    Task<Guid> CreateVehicleAsync(Vehicle car);

    Task UpdateCarAsync(Vehicle car);

    Task DeleteCarAsync(Guid id);
}
