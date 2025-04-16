namespace DomainTests;

using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Factories;
using FluentAssertions;

public class VehicleFactoryTests
{
    private readonly VehicleFactory _factory = new();

    public static IEnumerable<object[]> VehicleTypeToExpectedType => new List<object[]>
    {
        new object[] { VehicleType.Hatchback, typeof(Hatchback) },
        new object[] { VehicleType.Sudan, typeof(Sudan) },
        new object[] { VehicleType.Truck, typeof(Truck) },
        new object[] { VehicleType.SUV, typeof(Suv) }, 
    };


    [Theory]
    [MemberData(nameof(VehicleTypeToExpectedType))]
    public void Create_WhenVehicleTypeIsValid_ShouldReturnCorrectSubtype(VehicleType type, Type expectedType)
    {
        // Arrange
        var model = "Model X";
        var manufacturer = "Tesla";
        var year = 2023;
        var bid = 10000;
        var doors = 4;
        var capacity = 500;
        var seats = 5;

        // Act
        var vehicle = _factory.Create(type, model, manufacturer, year, bid, doors, capacity, seats);

        // Assert
        vehicle.Should().NotBeNull();
        vehicle.Type.Should().Be(type);
        vehicle.Model.Should().Be(model);
        vehicle.Manufacturer.Should().Be(manufacturer);
        vehicle.Should().BeOfType(expectedType);
    }

    [Fact]
    public void Create_WhenVehicleTypeIsInvalid_ShouldThrow()
    {
        // Arrange
        var invalidType = (VehicleType)999;

        // Act
        Action act = () => _factory.Create(invalidType, "model", "brand", 2024, 1000, 4, 500, 5);

        // Assert
        act.Should().Throw<BusinessRuleException>()
           .WithMessage("Vehicle type not supported");
    }
}
