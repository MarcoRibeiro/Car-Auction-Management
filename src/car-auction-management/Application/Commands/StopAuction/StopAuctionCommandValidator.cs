namespace Application.Commands.StopAuction;

using FluentValidation;

public class StopAuctionCommandValidator : AbstractValidator<StopAuctionCommand>
{
    public StopAuctionCommandValidator() 
    {
        RuleFor(x => x.VehicleId).NotEmpty();
    }
}
