using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infrastructure;

namespace InfrastructureTests;

public class VehicleRepositoryTests
{
    private readonly VehicleRepository _vehicleRepository;
    private readonly Fixture _fixture;

    public VehicleRepositoryTests() 
    {
        _vehicleRepository = new VehicleRepository();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task CreateAsync_ShouldStoreEntity()
    {
        // Arrange
        var vehicle = _fixture.Create<Truck>();

        // Act
        var id = await _vehicleRepository.CreateAsync(vehicle);

        // Assert
        id.Should().Be(vehicle.Id);

        var result = await _vehicleRepository.GetByIdAsync(id);
        result.Should().BeEquivalentTo(vehicle);
    }

    [Fact]
    public async Task GetAllAsync_WhenNoPredicate_ShouldReturnAll()
    {
        // Arrange
        var vehicleTruck = _fixture.Create<Truck>();
        var vehicleSudan = _fixture.Create<Sudan>();

        await _vehicleRepository.CreateAsync(vehicleTruck);
        await _vehicleRepository.CreateAsync(vehicleSudan);

        // Act
        var result = await _vehicleRepository.GetAllAsync();

        // Assert
        result.Should().BeEquivalentTo(new List<Vehicle> { vehicleTruck, vehicleSudan });
    }

    [Fact]
    public async Task GetAllAsync_ShouldFilter_WhenPredicateProvided()
    {
        // Arrange
        var vehicleTruck = _fixture.Create<Truck>();
        var vehicleSudan = _fixture.Create<Sudan>();

        await _vehicleRepository.CreateAsync(vehicleTruck);
        await _vehicleRepository.CreateAsync(vehicleSudan);

        // Act
        var result = await _vehicleRepository.GetAllAsync(x => x.Type == Domain.Enums.VehicleType.Sudan);

        // Assert
        result.Should().ContainSingle().Which.Should().BeEquivalentTo(vehicleSudan);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnEntity_WhenExists()
    {
        // Arrange
        var vehicle = _fixture.Create<Suv>();
        await _vehicleRepository.CreateAsync(vehicle);

        // Act
        var result = await _vehicleRepository.GetByIdAsync(vehicle.Id);

        // Assert
        result.Should().BeEquivalentTo(vehicle);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = await _vehicleRepository.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntity()
    {
        // Arrange
        var vehicle = _fixture.Create<Suv>();
        await _vehicleRepository.CreateAsync(vehicle);

        // Act
        await _vehicleRepository.DeleteAsync(vehicle.Id);

        // Assert
        var result = await _vehicleRepository.GetByIdAsync(vehicle.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReplaceEntity_WhenExists()
    {
        // Arrange
        var modelNew = "wow";
        var vehicle = _fixture.Create<Suv>();
        await _vehicleRepository.CreateAsync(vehicle);

        var updated = _fixture.Build<Suv>()
            .With(x => x.Id, vehicle.Id)
            .With(x => x.Model, modelNew)
            .Create();

        // Act
        await _vehicleRepository.UpdateAsync(updated);

        // Assert
        var result = await _vehicleRepository.GetByIdAsync(vehicle.Id);
        result.Model.Should().Be(modelNew);
    }
}