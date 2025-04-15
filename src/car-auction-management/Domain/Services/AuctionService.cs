namespace Domain.Services;

using System;
using System.Threading.Tasks;

using Domain.Interfaces;
using Domain.Exceptions;
using Domain.Entities;
using Domain.Enums;

public class AuctionService(IVehicleRepository vehicleRepository, IAuctionRepository auctionRepository) : IAuctionService
{
    private readonly string auctionAlreadyExistsMessage = "Already exists an active auction for this vehicle";
    private readonly string notActiveAuctionMessage = "There is not active auction for the given vehicle";
    private readonly string vehicleNotExistsMessage = "Vehicle doesn't exists in the inventory";
    private readonly string valueBellowStartingBidMessage = "Bib amount is bellow the starting bid amount";
    private readonly string valueBellowCurrentHighestBidMessage = "Bid amount must be greater than the current highest bid";

    public async Task<Guid> StartAuctionAsync(Guid vehicleId)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(vehicleId);
        if (vehicle == null)
        {
            throw new BusinessRuleException(vehicleNotExistsMessage);
        }

        IEnumerable<Auction> allActiveAuctions = await GetActiveAuctionsAsync(vehicleId);
        if (allActiveAuctions.Any())
        {
            throw new BusinessRuleException(auctionAlreadyExistsMessage);
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
            throw new BusinessRuleException(notActiveAuctionMessage);
        }

        var auction = allActiveAuctions.First();

        if (value <= auction.StartingBid)
        {
            throw new BusinessRuleException(valueBellowStartingBidMessage);
        }

        if (auction.CurrentBid.HasValue && value <= auction.CurrentBid.Value)
        {
            throw new BusinessRuleException(valueBellowCurrentHighestBidMessage);
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
            throw new BusinessRuleException(notActiveAuctionMessage);
        }

        if (auction.CurrentBid.HasValue && auction.CurrentBid.Value >= auction.StartingBid)
        {
            auction.Status = AuctionStatus.Sold;
            await auctionRepository.UpdateAsync(auction);
            return;
        }

        auction.Status = AuctionStatus.Inactive;
        await auctionRepository.UpdateAsync(auction);
    }

    private async Task<IEnumerable<Auction>> GetActiveAuctionsAsync(Guid vehicleId)
    {
        return await auctionRepository.GetAllAsync(auction => auction.VehicleId == vehicleId && auction.Status == AuctionStatus.Active);
    }
}
