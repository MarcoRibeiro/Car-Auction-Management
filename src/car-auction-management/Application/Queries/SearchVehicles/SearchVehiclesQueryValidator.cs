namespace Application.Queries.SearchVehicles;

using FluentValidation;

public class SearchVehiclesQueryValidator : AbstractValidator<SearchVehiclesQuery>
{
    public SearchVehiclesQueryValidator()
    {
        RuleFor(x => x.Type).IsInEnum().When(x => x.Type.HasValue);
    }
}