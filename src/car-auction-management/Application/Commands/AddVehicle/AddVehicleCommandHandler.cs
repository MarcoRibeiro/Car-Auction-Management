namespace Application.Commands.AddVehicle;

using Domain.Interfaces;

using MediatR;

public class AddVehicleCommandHandler(IVehicleRepository vehicleRepository, IVehicleFactory vehicleFactory) 
    : IRequestHandler<AddVehicleCommand, Guid>
{
    public async Task<Guid> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = vehicleFactory.Create(
            request.Type,
            request.Model,
            request.Manufacturer,
            request.Year,
            request.Startingid,
            request.NumberOfDoors,
            request.LoadCapacity,
            request.NumberOfSeats);

        return await vehicleRepository.CreateAsync(vehicle);
    }
}
