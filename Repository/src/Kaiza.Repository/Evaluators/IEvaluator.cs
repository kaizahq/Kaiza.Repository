namespace Kaiza.Repository.Evaluators;

public interface IEvaluator
{
    bool IsCriteriaEvaluator { get; }

    IQueryable<T> Evaluate<T>(IQueryable<T> query, SpecificationQuery<T> specificationQuery) where T : class;
}