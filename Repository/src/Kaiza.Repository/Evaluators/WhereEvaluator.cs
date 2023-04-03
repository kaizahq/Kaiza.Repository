namespace Kaiza.Repository.Evaluators;

public class WhereEvaluator : IEvaluator
{
    public static WhereEvaluator Instance { get; } = new WhereEvaluator();

    public bool IsCriteriaEvaluator { get; } = true;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        foreach (var expression in specificationQuery.WhereExpressions)
        {
            query = query.Where(expression.Filter);
        }

        return query;
    }
}