using Kaiza.Repository.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Kaiza.Repository.EntityFrameworkCore.Evaluators;

public class AsNoTrackingEvaluator : IEvaluator
{
    public static AsNoTrackingEvaluator Instance { get; } = new AsNoTrackingEvaluator();

    public bool IsCriteriaEvaluator { get; } = true;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        if (specificationQuery.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
}