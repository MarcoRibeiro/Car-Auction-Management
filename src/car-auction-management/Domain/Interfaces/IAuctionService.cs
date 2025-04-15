namespace Domain.Interfaces;

public interface IAuctionService
{
    Task StartAuctionAsync(Guid vehicleId);

    Task StopAuctionAsync(Guid vehicleId);

    Task PlaceBidAsync(Guid vehicleId, double value);
}
