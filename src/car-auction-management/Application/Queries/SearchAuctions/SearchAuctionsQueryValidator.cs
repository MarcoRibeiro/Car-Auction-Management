namespace Application.Queries.SearchAuctions;

using FluentValidation;

public class SearchAuctionsQueryValidator : AbstractValidator<SearchAuctionsQuery>
{
    public SearchAuctionsQueryValidator()
    {
        RuleFor(x => x.VehicleId).NotEmpty().When(x => x.VehicleId.HasValue);
        RuleFor(x => x.Status).IsInEnum().When(x => x.Status.HasValue);
    }
}