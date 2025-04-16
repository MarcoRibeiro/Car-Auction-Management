namespace ApplicationTests.AddVehicle;

using Application.Commands.AddVehicle;
using Domain.Enums;
using FluentValidation.TestHelper;
using Xunit;

public class AddVehicleCommandValidatorTests
{
    private readonly AddVehicleCommandValidator _validator = new();

    [Fact]
    public void AddVehicleCommand_WhenRequiredFieldsAreMissing_ShouldHaveError()
    {
        var command = new AddVehicleCommand 
        {
            Type = (VehicleType)10,
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Model);
        result.ShouldHaveValidationErrorFor(x => x.Manufacturer);
        result.ShouldHaveValidationErrorFor(x => x.Year);
        result.ShouldHaveValidationErrorFor(x => x.Type);
        result.ShouldHaveValidationErrorFor(x => x.StartingBid);
    }

    [Fact]
    public void AddVehicleCommand_WithCorrectlySudan_ShouldBeValid()
    {
        var command = new AddVehicleCommand
        {
            Model = "S",
            Manufacturer = "Tesla",
            Year = 2020,
            Type = VehicleType.Sudan,
            StartingBid = 1000,
            NumberOfDoors = 4
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void AddVehicleCommand_WithCorrectlySuv_ShouldBeValid()
    {
        var command = new AddVehicleCommand
        {
            Model = "X",
            Manufacturer = "Tesla",
            Year = 2020,
            Type = VehicleType.SUV,
            StartingBid = 1000,
            NumberOfSeats = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.NumberOfSeats);
    }

    [Fact]
    public void AddVehicleCommand_WithCorrectlyTruck_ShouldBeValid()
    {
        var command = new AddVehicleCommand
        {
            Model = "Heavy",
            Manufacturer = "Volvo",
            Year = 2018,
            Type = VehicleType.Truck,
            StartingBid = 1500,
            LoadCapacity = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.LoadCapacity);
    }

    [Fact]
    public void AddVehicleCommand_WhenNotSudanOrHatchback_ShouldNotRequireNumberOfDoors()
    {
        var command = new AddVehicleCommand
        {
            Model = "X",
            Manufacturer = "Tesla",
            Year = 2021,
            Type = VehicleType.SUV,
            StartingBid = 1500,
            NumberOfDoors = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.NumberOfDoors);
    }

    [Fact]
    public void AddVehicleCommand_WhenNotTrunk_ShouldNotRequireLoadCapacity()
    {
        var command = new AddVehicleCommand
        {
            Model = "S",
            Manufacturer = "Tesla",
            Year = 2021,
            Type = VehicleType.SUV,
            StartingBid = 1500,
            LoadCapacity = 0
        };

        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.LoadCapacity);
    }
}
