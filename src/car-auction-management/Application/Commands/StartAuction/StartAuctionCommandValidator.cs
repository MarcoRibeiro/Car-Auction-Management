namespace Application.Commands.StartAuction;

using FluentValidation;

public class StartAuctionCommandValidator : AbstractValidator<StartAuctionCommand>
{
    public StartAuctionCommandValidator() 
    {
        RuleFor(x => x.VehicleId).NotEmpty();
    }
}
