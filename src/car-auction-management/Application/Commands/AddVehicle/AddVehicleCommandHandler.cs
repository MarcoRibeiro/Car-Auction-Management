namespace Application.Commands.AddVehicle;

using Domain.Interfaces.Repositories;

using MediatR;

public class AddVehicleCommandHandler(IVehicleRepository vehicleRepository) 
    : IRequestHandler<AddVehicleCommand, Guid>
{
    public Task<Guid> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
