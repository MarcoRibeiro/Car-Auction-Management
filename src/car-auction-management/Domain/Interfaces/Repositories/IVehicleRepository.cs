namespace Domain.Interfaces.Repositories;

using Domain.Entities;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllCarsAsync();

    Task<Vehicle> GetVehicleByIdAsync(Guid id);

    Task CreateVehicleAsync(Vehicle car);

    Task UpdateCarAsync(Vehicle car);

    Task DeleteCarAsync(int id);
}
