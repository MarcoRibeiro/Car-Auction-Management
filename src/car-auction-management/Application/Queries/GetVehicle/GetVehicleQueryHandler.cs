﻿namespace Application.Queries.GetVehicle;

using Domain.Entities;
using Domain.Interfaces.Repositories;

using MediatR;

public record GetVehicleQueryHandler(IVehicleRepository vehicleRepository) :
    IRequestHandler<GetVehicleQuery, Vehicle>
{
    public async Task<Vehicle> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        return await this.vehicleRepository.GetVehicleByIdAsync(request.Id);
    }
}
