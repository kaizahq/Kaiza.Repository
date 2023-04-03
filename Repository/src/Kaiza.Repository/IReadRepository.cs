namespace Kaiza.Repository;

public interface IReadRepository<T> where T : class
{
    Task<T?> GetById<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    Task<T?> FirstOrDefault(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<T?> SingleOrDefault(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<List<T>> List(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<int> Count(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<bool> Any(ISpecification<T> specification, CancellationToken cancellationToken = default);

}