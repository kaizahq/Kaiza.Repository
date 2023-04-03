using Kaiza.Repository.Evaluators;

namespace Kaiza.Repository.EntityFrameworkCore.Evaluators;

public class SpecificationEvaluator : ISpecificationEvaluator
{
    public static SpecificationEvaluator Default { get; } = new SpecificationEvaluator();

    private readonly List<IEvaluator> _evaluators = new();

    public SpecificationEvaluator()
    {
        _evaluators.AddRange(new IEvaluator[]
        {
                WhereEvaluator.Instance,
                IncludeEvaluator.Instance,
                OrderEvaluator.Instance,
                PaginationEvaluator.Instance,
                AsNoTrackingEvaluator.Instance,
                IgnoreQueryFiltersEvaluator.Instance,
                AsSplitQueryEvaluator.Instance
        });
    }
    
    public SpecificationEvaluator(IEnumerable<IEvaluator> evaluators)
    {
        _evaluators.AddRange(evaluators);
    }

    public IQueryable<T> GetQuery<T>(IQueryable<T> query, ISpecification<T> specification, bool evaluateCriteriaOnly = false) where T : class
    {
        if (specification is null) throw new ArgumentNullException("Specification is required");

        var specificationQuery = specification.GetQuery();

        var evaluators = evaluateCriteriaOnly
            ? _evaluators.Where(x => x.IsCriteriaEvaluator)
            : _evaluators;

        foreach (var evaluator in evaluators)
        {
            query = evaluator.Evaluate(query, specificationQuery);
        }

        return query;
    }
}
