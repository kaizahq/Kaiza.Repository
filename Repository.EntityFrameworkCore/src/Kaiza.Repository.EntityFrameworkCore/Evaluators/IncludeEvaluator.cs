using Kaiza.Repository.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Kaiza.Repository.EntityFrameworkCore.Evaluators;

public class IncludeEvaluator : IEvaluator
{
    public static IncludeEvaluator Instance { get; } = new IncludeEvaluator();

    public bool IsCriteriaEvaluator { get; } = true;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class
    {
        foreach (var includeString in specificationQuery.IncludeStrings)
        {
            query = query.Include(includeString);
        }

        return query;
    }
}