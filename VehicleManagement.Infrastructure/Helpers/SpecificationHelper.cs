using VehicleManagement.DomainService.BaseSpecifications;

namespace VehicleManagement.Infrastructure.Helpers;

public static class SpecificationHelper
{
    public static IQueryable<TEntity> Specify<TEntity>(this IQueryable<TEntity> query, BaseSpecification<TEntity> specification)
    {
        var queryable = query;

        if (specification.Criteria != null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        if (specification.OrderByExpression != null)
        {
            queryable = queryable.OrderBy(specification.OrderByExpression);
        }

        if (specification.OrderByDescendingExpression != null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        return queryable;
    }
}
