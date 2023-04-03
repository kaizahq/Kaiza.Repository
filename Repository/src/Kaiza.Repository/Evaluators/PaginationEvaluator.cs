namespace Kaiza.Repository.Evaluators;

public class PaginationEvaluator : IEvaluator
{
    public static PaginationEvaluator Instance { get; } = new PaginationEvaluator();

    public bool IsCriteriaEvaluator { get; } = false;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        if (specificationQuery.PageIndex == null || specificationQuery.PageSize == null) return query;

        if (specificationQuery.PageIndex > 0)
        {
            query = query.Skip(specificationQuery.PageIndex.Value * specificationQuery.PageSize.Value);
        }

        query = query.Take(specificationQuery.PageSize.Value);

        return query;
    }
}