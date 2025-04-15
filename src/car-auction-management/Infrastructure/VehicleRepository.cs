namespace Infrastructure;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Interfaces;

public class VehicleRepository : IVehicleRepository
{
    private readonly List<Vehicle> vehicles = new();

    public Task<Guid> CreateVehicleAsync(Vehicle vehicle)
    {
        vehicle.Id = Guid.NewGuid();
        this.vehicles.Add(vehicle);
        return Task.FromResult(vehicle.Id);
    }

    public Task DeleteCarAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Vehicle>> GetAllCarsAsync()
    {
        return Task.FromResult((IEnumerable<Vehicle>)this.vehicles);
    }

    public Task<Vehicle> GetVehicleByIdAsync(Guid id)
    {
        return Task.FromResult(this.vehicles.Find(x => x.Id == id));
    }

    public Task UpdateCarAsync(Vehicle vehicle)
    {
        throw new NotImplementedException();
    }
}
