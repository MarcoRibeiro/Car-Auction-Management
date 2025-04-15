namespace Application.Queries.SearchVehicles;

using System.Linq.Expressions;

using Domain.Entities;
using Domain.Interfaces;

using MediatR;

public record SearchVehiclesQueryHandler(IVehicleRepository vehicleRepository) :
    IRequestHandler<SearchVehiclesQuery, IEnumerable<Vehicle>>
{
    public async Task<IEnumerable<Vehicle>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await vehicleRepository.GetAllAsync(request.ToExpression());
    }
}