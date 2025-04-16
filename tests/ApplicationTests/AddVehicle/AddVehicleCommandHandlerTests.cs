namespace ApplicationTests.AddVehicle;

using Application.Commands.AddVehicle;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using FluentAssertions;
using NSubstitute;


public class AddVehicleCommandHandlerTests
{
    private readonly IVehicleFactory _factory;
    private readonly IVehicleRepository _repository;
    private readonly AddVehicleCommandHandler _handler;

    public AddVehicleCommandHandlerTests()
    {
        _factory = Substitute.For<IVehicleFactory>();
        _repository = Substitute.For<IVehicleRepository>();
        _handler = new AddVehicleCommandHandler(_repository, _factory);
    }

    [Fact]
    public async Task Handle_ShouldCreateVehicleAndReturnId()
    {
        // Arrange
        var command = new AddVehicleCommand
        {
            Type = VehicleType.SUV,
            Model = "Model X",
            Manufacturer = "Tesla",
            Year = 2023,
            StartingBid = 15000,
            NumberOfDoors = 5,
            LoadCapacity = 0,
            NumberOfSeats = 7
        };

        var expectedVehicle = new Suv
        {
            Id = Guid.NewGuid(),
            Type = command.Type,
            Model = command.Model,
            Manufacturer = command.Manufacturer,
            Year = command.Year,
            StartingBid = command.StartingBid,
            NumberOfSeats = command.NumberOfSeats
        };

        _factory.Create(
            command.Type,
            command.Model,
            command.Manufacturer,
            command.Year,
            command.StartingBid,
            command.NumberOfDoors,
            command.LoadCapacity,
            command.NumberOfSeats
        ).Returns(expectedVehicle);

        _repository.CreateAsync(expectedVehicle).Returns(expectedVehicle.Id);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().Be(expectedVehicle.Id);
        await _repository.Received(1).CreateAsync(expectedVehicle);
    }
}
