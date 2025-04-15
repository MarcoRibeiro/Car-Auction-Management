namespace Domain.Interfaces;

using Domain.Entities;

public interface IAuctionService
{
    Task ActiveAuctionAsync(Guid vehicleId);

    Task StopAuctionAsync(Guid vehicleId);

}
