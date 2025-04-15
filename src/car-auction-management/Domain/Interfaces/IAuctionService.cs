namespace Domain.Interfaces;

public interface IAuctionService
{
    Task<Guid> StartAuctionAsync(Guid vehicleId);

    Task StopAuctionAsync(Guid vehicleId);

    Task PlaceBidAsync(Guid vehicleId, double value);
}
