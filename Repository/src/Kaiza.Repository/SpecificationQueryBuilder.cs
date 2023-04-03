using Kaiza.Repository.Expressions;
using System.Linq.Expressions;

namespace Kaiza.Repository;

public class SpecificationQueryBuilder<T> where T : class
{
    private readonly SpecificationQuery<T> _query = new();

    public SpecificationQueryBuilder<T> AsNoTracking(bool condition = true)
    {
        if (condition)
        {
            _query.AsNoTracking = true;
        }

        return this;
    }

    public SpecificationQueryBuilder<T> AsSplitQuery(bool condition = true)
    {
        if (condition)
        {
            _query.AsSplitQuery = true;
        }

        return this;
    }

    public SpecificationQueryBuilder<T> IgnoreQueryFilters(bool condition = true)
    {
        if (condition)
        {
            _query.IgnoreQueryFilters = true;
        }

        return this;
    }

    public SpecificationQueryBuilder<T> Page(int pageIndex, int pageSize, bool condition = true)
    {
        if (condition)
        {
            _query.PageIndex = pageIndex;
            _query.PageSize = pageSize;
        }

        return this;
    }

    public SpecificationQueryBuilder<T> Include(string include, bool condition = true)
    {
        if (condition)
        {
            _query.IncludeStrings.Add(include);
        }

        return this;
    }

    public SpecificationQueryBuilder<T> Where(Expression<Func<T, bool>> predicate, bool condition = true)
    {
        if (condition)
        {
            _query.WhereExpressions.Add(new WhereExpression<T>(predicate));
        }

        return this;
    }

    public SpecificationQueryBuilder<T> OrderBy(Expression<Func<T, object?>> keySelector, bool condition = true)
    {
        if (condition)
        {
            _query.OrderExpressions.Add(new OrderExpression<T>(keySelector, OrderDirection.Ascending));
        }

        return this;
    }

    public SpecificationQueryBuilder<T> OrderByDescending(Expression<Func<T, object?>> keySelector, bool condition = true)
    {
        if (condition)
        {
            _query.OrderExpressions.Add(new OrderExpression<T>(keySelector, OrderDirection.Descending));
        }

        return this;
    }

    public SpecificationQuery<T> Build() => _query;
}
