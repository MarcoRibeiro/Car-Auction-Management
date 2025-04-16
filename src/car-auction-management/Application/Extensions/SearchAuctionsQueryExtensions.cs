namespace Application.Extensions;

using Application.Queries.SearchAuctions;
using Application.Queries.SearchVehicles;

using Domain.Entities;

using System.Linq.Expressions;

public static class SearchAuctionsQueryExtensions
{
    public static Expression<Func<Auction, bool>> ToExpression(this SearchAuctionsQuery filter)
    {
        var parameter = Expression.Parameter(typeof(Auction));
        Expression finalExpression = Expression.Constant(true);

        if (filter.VehicleId.HasValue)
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.VehicleId.Value, nameof(Auction.VehicleId), parameter);
        }

        if (filter.Status.HasValue)
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.Status.Value, nameof(Auction.Status), parameter);
        }

        return Expression.Lambda<Func<Auction, bool>>(finalExpression, parameter);
    }

    private static Expression AddEqualsExpression<T>(ref Expression finalExpression, T value, string property, ParameterExpression parameter)
    {
        var modelProp = Expression.Property(parameter, property);
        var modelValue = Expression.Constant(value);
        var condition = Expression.Equal(modelProp, modelValue);
        finalExpression = Expression.AndAlso(finalExpression, condition);

        return finalExpression;
    }
}
