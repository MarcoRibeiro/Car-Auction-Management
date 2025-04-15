namespace Application.Commands.AddVehicle;

using Domain.Enums;

using FluentValidation;

public class AddVehicleCommandValidator : AbstractValidator<AddVehicleCommand>
{
    public AddVehicleCommandValidator() 
    {
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.Manufacturer).NotEmpty();
        RuleFor(x => x.Year).NotEmpty().InclusiveBetween(1900, DateTime.Now.Year);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.Startingid).NotEmpty().GreaterThan(0);
        RuleFor(x => x.NumberOfDoors)
            .NotEmpty().InclusiveBetween(3, 5).When(x => x.Type == VehicleType.Sudan || x.Type == VehicleType.Hatchback);
        RuleFor(x => x.NumberOfDoors).Empty().When(x => x.Type != VehicleType.Sudan || x.Type != VehicleType.Hatchback);
        RuleFor(x => x.NumberOfSeats).NotEmpty().InclusiveBetween(3, 7).When(x => x.Type == VehicleType.SUV);
        RuleFor(x => x.NumberOfDoors).Empty().When(x => x.Type != VehicleType.SUV);
        RuleFor(x => x.NumberOfDoors).NotEmpty().GreaterThan(0).When(x => x.Type != VehicleType.Truck);
        RuleFor(x => x.NumberOfDoors).Empty().When(x => x.Type != VehicleType.Truck);
    }
}
