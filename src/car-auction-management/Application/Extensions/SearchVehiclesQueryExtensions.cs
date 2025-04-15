namespace Application.Extensions;

using Application.Queries.SearchVehicles;

using Domain.Entities;
using System.Linq.Expressions;

public static class SearchVehiclesQueryExtensions
{
    public static Expression<Func<Vehicle, bool>> ToExpression(this SearchVehiclesQuery filter)
    {
        var parameter = Expression.Parameter(typeof(Vehicle));
        Expression finalExpression = Expression.Empty();

        if (!string.IsNullOrWhiteSpace(filter.Model))
        {
            var modelProp = Expression.Property(parameter, nameof(Vehicle.Model));
            var modelValue = Expression.Constant(filter.Model);
            var condition = Expression.Equal(modelProp, modelValue);
            finalExpression = Expression.AndAlso(finalExpression, condition);
        }

        if (!string.IsNullOrWhiteSpace(filter.Manufacturer))
        {
            var modelProp = Expression.Property(parameter, nameof(Vehicle.Manufacturer));
            var modelValue = Expression.Constant(filter.Manufacturer);
            var condition = Expression.Equal(modelProp, modelValue);
            finalExpression = Expression.AndAlso(finalExpression, condition);
        }

        if (filter.Year.HasValue)
        {
            var modelProp = Expression.Property(parameter, nameof(Vehicle.Year));
            var modelValue = Expression.Constant(filter.Year.Value);
            var condition = Expression.Equal(modelProp, modelValue);
            finalExpression = Expression.AndAlso(finalExpression, condition);
        }

        if (filter.Type.HasValue)
        {
            var modelProp = Expression.Property(parameter, nameof(Vehicle.Year));
            var modelValue = Expression.Constant(filter.Type.Value);
            var condition = Expression.Equal(modelProp, modelValue);
            finalExpression = Expression.AndAlso(finalExpression, condition);
        }

        return Expression.Lambda<Func<Vehicle, bool>>(finalExpression, parameter);
    }
}
