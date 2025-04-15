namespace Application.Commands.StopAuction;

using Domain.Interfaces;

using MediatR;

public class StopAuctionCommandHandler(IAuctionService auctionService) 
    : IRequestHandler<StopAuctionCommand>
{
    public async Task Handle(StopAuctionCommand request, CancellationToken cancellationToken)
    {
        await auctionService.StopAuctionAsync(request.VehicleId);
    }
}
