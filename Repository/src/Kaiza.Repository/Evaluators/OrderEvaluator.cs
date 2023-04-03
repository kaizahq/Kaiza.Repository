using Kaiza.Repository.Expressions;

namespace Kaiza.Repository.Evaluators;

public class OrderEvaluator : IEvaluator
{
    public static OrderEvaluator Instance { get; } = new OrderEvaluator();

    public bool IsCriteriaEvaluator { get; } = false;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        IOrderedQueryable<T>? orderedQuery = null;
        foreach (var orderExpression in specificationQuery.OrderExpressions)
        {
            if (orderExpression.Direction == OrderDirection.Ascending)
            {
                orderedQuery = orderedQuery == null
                    ? query.OrderBy(orderExpression.KeySelector)
                    : orderedQuery.ThenBy(orderExpression.KeySelector);
            }
            else if (orderExpression.Direction == OrderDirection.Descending)
            {
                orderedQuery = orderedQuery == null
                    ? query.OrderByDescending(orderExpression.KeySelector)
                    : orderedQuery.ThenByDescending(orderExpression.KeySelector);
            }
        }

        return orderedQuery ?? query;
    }
}