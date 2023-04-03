using Kaiza.Repository.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Kaiza.Repository.EntityFrameworkCore.Evaluators;

public class AsSplitQueryEvaluator : IEvaluator
{
    public static AsSplitQueryEvaluator Instance { get; } = new AsSplitQueryEvaluator();

    public bool IsCriteriaEvaluator { get; } = true;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        if (specificationQuery.AsSplitQuery)
        {
            query = query.AsSplitQuery();
        }

        return query;
    }
}