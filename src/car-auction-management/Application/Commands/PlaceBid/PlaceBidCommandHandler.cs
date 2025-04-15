namespace Application.Commands.PlaceBid;

using Domain.Interfaces;

using MediatR;

public class PlaceBidCommandHandler(IAuctionService auctionService) 
    : IRequestHandler<PlaceBidCommand>
{
    public async Task Handle(PlaceBidCommand request, CancellationToken cancellationToken)
    {
        await auctionService.PlaceBidAsync(request.VehicleId, request.Amount);
    }
}
