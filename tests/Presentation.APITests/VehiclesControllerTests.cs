namespace Presentation.APITests;

using Application.Commands.AddVehicle;
using Application.Commands.PlaceBid;
using Application.Commands.StartAuction;
using Application.Commands.StopAuction;
using Application.Queries.GetVehicle;
using Application.Queries.SearchAuctions;
using Application.Queries.SearchVehicles;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Presentation.API.Controllers.V1;
using Presentation.API.Controllers.V1.DTOs;
using Xunit;

public class VehiclesControllerTests
{
    private readonly ISender _sender;
    private readonly ILogger<VehiclesController> _logger;
    private readonly VehiclesController _controller;
    private readonly Fixture _fixture;

    public VehiclesControllerTests()
    {
        _fixture = new Fixture();
        _sender = Substitute.For<ISender>();
        _logger = Substitute.For<ILogger<VehiclesController>>();
        _controller = new VehiclesController(_sender, _logger);
    }

    [Fact]
    public async Task GetAsync_WhenExists_ShouldReturnVehicle()
    {
        // Arrange
        var id = Guid.NewGuid();
        var vehicle = _fixture.Create<Suv>();
        _sender.Send(Arg.Any<GetVehicleQuery>()).Returns(vehicle);

        // Act
        var result = await _controller.GetAsync(id);

        // Assert
        await _sender.Received(1).Send(Arg.Is<GetVehicleQuery>(q => q.Id == id));
        result.Value.Should().BeEquivalentTo(vehicle);
        result.Value.Should().BeOfType<VehicleDto>();
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFound_WhenNotExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        _sender.Send(Arg.Any<GetVehicleQuery>()).Returns((Vehicle?)null);

        // Act
        var result = await _controller.GetAsync(id);

        // Assert
        await _sender.Received(1).Send(Arg.Is<GetVehicleQuery>(q => q.Id == id));
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldSendSearchVehiclesQuery()
    {
        // Arrange
        var vehicles = new List<Vehicle> { _fixture.Create<Suv>(), _fixture.Create<Suv>() };
        _sender
            .Send(Arg.Is<SearchVehiclesQuery>(x =>
                x.Model == "Model X" &&
                x.Manufacturer == "Tesla" &&
                x.Year == 2023 &&
                x.Type == Domain.Enums.VehicleType.SUV))
            .Returns(vehicles);

        // Act
        var result = await _controller.GetAllAsync("Model X", "Tesla", 2023, API.Controllers.V1.DTOs.VehicleType.SUV);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        ((OkObjectResult)result.Result!).Value.Should().BeEquivalentTo(vehicles);
    }

    [Fact]
    public async Task PostAsync_ShouldSendAddVehicleCommand()
    {
        // Arrange
        var request = new CreateVehicleRequest
        {
            Model = "X",
            Manufacturer = "Tesla",
            Year = 2023,
            StartingPrice = 10000,
            Type = API.Controllers.V1.DTOs.VehicleType.SUV,
            NumberOfSeats = 7
        };

        var newId = Guid.NewGuid();
        _sender.Send(Arg.Any<AddVehicleCommand>()).Returns(newId);

        // Act
        var result = await _controller.PostAsync(request);

        // Assert
        await _sender.Received(1).Send(Arg.Is<AddVehicleCommand>(cmd =>
            cmd.Model == request.Model && 
            cmd.Manufacturer == request.Manufacturer &&
            cmd.Year == request.Year &&
            cmd.StartingBid == request.StartingPrice &&
            cmd.NumberOfSeats == request.NumberOfSeats &&
            cmd.Type == Domain.Enums.VehicleType.SUV
        ));

        result.Value.Should().Be(newId);
    }

    [Fact]
    public async Task GetAllVehicleAuctionsAsync_ShouldReturnMappedDtos()
    {
        // Arrange
        var id = Guid.NewGuid();
        var auctions = new[] { new Auction { Id = Guid.NewGuid() } };
        _sender.Send(Arg.Any<SearchAuctionsQuery>()).Returns(auctions);

        // Act
        var result = await _controller.GetAllVehicleAuctionsAsync(id, null);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task StartAsync_ShouldSendStartAuctionCommand()
    {
        // Arrange
        var id = Guid.NewGuid();
        _sender.Send(Arg.Any<StartAuctionCommand>()).Returns(id);

        // Act
        var result = await _controller.StartAsync(id);

        // Assert
        await _sender.Received(1).Send(Arg.Is<StartAuctionCommand>(cmd => cmd.VehicleId == id));
        result.Value.Should().Be(id);
    }

    [Fact]
    public async Task StopAsync_ShouldSendStopAuctionComman()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await _controller.StopAsync(id);

        // Assert
        await _sender.Received(1).Send(Arg.Is<StopAuctionCommand>(cmd => cmd.VehicleId == id));
        result.Should().BeOfType<AcceptedResult>();
    }

    [Fact]
    public async Task BidAsync_ShouldSendPlaceBidCommand()
    {
        // Arrange
        var id = Guid.NewGuid();
        var request = new PlaceBidRequest { Amount = 5000 };

        // Act
        var result = await _controller.BidAsync(id, request);

        // Assert
        await _sender.Received(1).Send(Arg.Is<PlaceBidCommand>(cmd =>
            cmd.VehicleId == id && cmd.Amount == request.Amount
        ));

        result.Should().BeOfType<AcceptedResult>();
    }
}