namespace Application.Queries.GetVehicle;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

public record SearchVehiclesQueryHandler(IVehicleRepository vehicleRepository) :
    IRequestHandler<SearchVehiclesQuery, Vehicle>
{
    public async Task<Vehicle> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await vehicleRepository.GetByIdAsync(request.Id);
    }
}
