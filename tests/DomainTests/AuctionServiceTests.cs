namespace DomainTests;

using System.Linq.Expressions;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

public class AuctionServiceTests
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly AuctionService _auctionService;
    private readonly Fixture _fixture;

    public AuctionServiceTests()
    {
        _auctionRepository = Substitute.For<IAuctionRepository>();
        _vehicleRepository = Substitute.For<IVehicleRepository>();

        _auctionService = new AuctionService(_vehicleRepository, _auctionRepository);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task StartAuctionAsync_WhenVehicleNotExist_ShouldTrowVehicleNotExistsException()
    {
        // Arrange
        _vehicleRepository.GetByIdAsync(Arg.Any<Guid>()).ReturnsNull();

        // Act
        var act = async () => await _auctionService.StartAuctionAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<VehicleNotExistsException>();
    }

    [Fact]
    public async Task StartAuctionAsync_WhenAuctionIsAlreadyActive_ShouldTrowAnAuctionAlreadyExistsException()
    {
        // Arrange
        var vehicle = _fixture.Create<Truck>();
        _vehicleRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(vehicle);

        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>())
            .Returns(_fixture.CreateMany<Auction>(1));

        // Act
        var act = async () => await _auctionService.StartAuctionAsync(vehicle.Id);

        // Assert
        await act.Should().ThrowAsync<AuctionAlreadyExistsException>();
    }

    [Fact]
    public async Task StartAuctionAsync_WhenNotExistsAuctionActive_ShouldCallTheRepository()
    {
        // Arrange
        var vehicle = _fixture.Create<Truck>();
        _vehicleRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(vehicle);

        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>()).Returns([]);

        // Act
        var result = await _auctionService.StartAuctionAsync(vehicle.Id);

        // Assert
        await _auctionRepository.Received(1).CreateAsync(
            Arg.Is<Auction>(a => 
                a.VehicleId == vehicle.Id && 
                a.Status == AuctionStatus.Active &&
                a.StartingBid == vehicle.StartingBid));
    }

    [Fact]
    public async Task PlaceBidAsync_WhenNotExistsActiveAuction_ShouldThrowNotActiveAuctionException()
    {
        // Arrange
        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>()).Returns([]);

        // Act
        var act = async () => await _auctionService.PlaceBidAsync(Guid.NewGuid(), 1000);

        // Assert
        await act.Should().ThrowAsync<NotActiveAuctionException>();
    }

    [Fact]
    public async Task PlaceBidAsync_WhenBidIsBellowStartingBid_ShouldThrowBidBellowStartingBidException()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var bidBellow = 500;
        var auction = _fixture.Build<Auction>().With(a => a.StartingBid, 1000).Create();
        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>())
            .Returns([auction]);

        // Act
        var act = async () => await _auctionService.PlaceBidAsync(vehicleId, bidBellow);

        // Assert
        await act.Should().ThrowAsync<BidBellowStartingBidException>();
    }

    [Fact]
    public async Task PlaceBidAsync_WhenBidBellowCurrentHighestBid_ShouldThrowBidBellowCurrentHighestBidException()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var bidBellow = 1500;
        var auction = _fixture.Build<Auction>()
            .With(a => a.StartingBid, 1000)
            .With(a => a.CurrentBid, 1600)
            .Create();
        
        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>())
            .Returns([auction]);

        // Act
        var act = async () => await _auctionService.PlaceBidAsync(vehicleId, bidBellow);

        // Assert
        await act.Should().ThrowAsync<BidBellowCurrentHighestBidException>();
    }

    [Fact]
    public async Task PlaceBidAsync_WhenNotExistsAuctionActive_ShouldCallTheRepository()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var validBid = 1500;
        var auction = _fixture.Build<Auction>()
            .With(a => a.StartingBid, 1000)
            .With(a => a.CurrentBid, 1300)
            .Create();

        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>())
            .Returns([auction]);

        // Act
        await _auctionService.PlaceBidAsync(vehicleId, validBid);

        // Assert
        await _auctionRepository.Received(1).UpdateAsync(
            Arg.Is<Auction>(a =>
                a.VehicleId == vehicleId &&
                a.CurrentBid == validBid));
    }

    [Fact]
    public async Task StopAuctionAsync_WhenNotExistsActiveAuction_ShouldThrowNotActiveAuctionException()
    {
        // Arrange
        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>()).Returns([]);

        // Act
        var act = async () => await _auctionService.StopAuctionAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<NotActiveAuctionException>();
    }

    [Fact]
    public async Task StopAuctionAsync_WhenExistsAuctionActive_ShouldUpdateTheAuction()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var auction = _fixture.Build<Auction>().Create();

        _auctionRepository.GetAllAsync(Arg.Any<Expression<Func<Auction, bool>>>())
            .Returns([auction]);

        // Act
        await _auctionService.StopAuctionAsync(vehicleId);

        // Assert
        await _auctionRepository.Received(1).UpdateAsync(
            Arg.Is<Auction>(a =>
                a.VehicleId == vehicleId &&
                a.Status == AuctionStatus.Inactive));
    }
}