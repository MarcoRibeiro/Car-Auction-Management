﻿namespace Infrastructure;

using Domain.Entities;
using Domain.Interfaces;

public class VehicleRepository : MemoryRepository<Vehicle>, IVehicleRepository
{
}
