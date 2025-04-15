namespace Application.Commands.StartAuction;

using Domain.Interfaces;

using MediatR;

public class StartAuctionCommandHandler(IAuctionService auctionService) 
    : IRequestHandler<StartAuctionCommand, Guid>
{
    public async Task<Guid> Handle(StartAuctionCommand request, CancellationToken cancellationToken)
    {
        return await auctionService.StartAuctionAsync(request.VehicleId);
    }
}
