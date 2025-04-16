namespace Domain.Services;

using System;
using System.Threading.Tasks;

using Domain.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using Domain.Enums;

public class AuctionService(IVehicleRepository vehicleRepository, IAuctionRepository auctionRepository) : IAuctionService
{
    public async Task<Guid> StartAuctionAsync(Guid vehicleId)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(vehicleId);
        if (vehicle == null)
        {
            throw new VehicleNotExistsException();
        }

        IEnumerable<Auction> allActiveAuctions = await GetActiveAuctionsAsync(vehicleId);
        if (allActiveAuctions.Any())
        {
            throw new AuctionAlreadyExistsException();
        }

        var auction = new Auction
        {
            VehicleId = vehicleId,
            StartDate = DateTime.UtcNow,
            Status = AuctionStatus.Active,
            StartingBid = vehicle.StartingBid,
        };

        return await auctionRepository.CreateAsync(auction);
    }

    public async Task PlaceBidAsync(Guid vehicleId, double value)
    {
        var allActiveAuctions = await GetActiveAuctionsAsync(vehicleId);
        if (!allActiveAuctions.Any())
        {
            throw new NotActiveAuctionException();
        }

        var auction = allActiveAuctions.First();

        if (value <= auction.StartingBid)
        {
            throw new BidBellowStartingBidException();
        }

        if (auction.CurrentBid.HasValue && value <= auction.CurrentBid.Value)
        {
            throw new BidBellowCurrentHighestBidException();
        }

        auction.CurrentBid = value;
        await auctionRepository.UpdateAsync(auction);
    }

    public async Task StopAuctionAsync(Guid vehicleId)
    {
        var allActiveAuctions = await GetActiveAuctionsAsync(vehicleId);
        var auction = allActiveAuctions.FirstOrDefault();

        if (auction == null) 
        {
            throw new NotActiveAuctionException();
        }

        auction.EndDate = DateTime.UtcNow;
        auction.Status = AuctionStatus.Inactive;
        
        await auctionRepository.UpdateAsync(auction);
    }

    private async Task<IEnumerable<Auction>> GetActiveAuctionsAsync(Guid vehicleId)
    {
        return await auctionRepository.GetAllAsync(auction => auction.VehicleId == vehicleId && auction.Status == AuctionStatus.Active);
    }
}
