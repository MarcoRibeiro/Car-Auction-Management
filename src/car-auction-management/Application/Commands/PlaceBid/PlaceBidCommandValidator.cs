namespace Application.Commands.PlaceBid;

using FluentValidation;

public class PlaceBidCommandValidator : AbstractValidator<PlaceBidCommand>
{
    public PlaceBidCommandValidator() 
    {
        RuleFor(x => x.VehicleId).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
    }
}
