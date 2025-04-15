namespace Application.Extensions;

using Application.Queries.SearchVehicles;

using Domain.Entities;

using System.Linq.Expressions;

public static class SearchVehiclesQueryExtensions
{
    public static Expression<Func<Vehicle, bool>> ToExpression(this SearchVehiclesQuery filter)
    {
        var parameter = Expression.Parameter(typeof(Vehicle));
        Expression finalExpression = Expression.Constant(true);

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.Manufacturer, nameof(Vehicle.Manufacturer), parameter);
        }

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.Model, nameof(Vehicle.Model), parameter);
        }

        if (filter.Year.HasValue)
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.Year.Value, nameof(Vehicle.Year), parameter);
        }

        if (filter.Type.HasValue)
        {
            finalExpression = AddEqualsExpression(ref finalExpression, filter.Type.Value, nameof(Vehicle.Type), parameter);
        }

        return Expression.Lambda<Func<Vehicle, bool>>(finalExpression, parameter);
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
