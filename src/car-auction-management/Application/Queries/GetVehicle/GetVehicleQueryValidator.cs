namespace Application.Queries.GetVehicle;

using FluentValidation;

public class GetVehicleQueryValidator : AbstractValidator<GetVehicleQuery>
{
    public GetVehicleQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}