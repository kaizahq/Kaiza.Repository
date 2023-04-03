namespace Kaiza.Repository;

public interface IRepository<T> : IReadRepository<T> where T : class
{
    Task Add(T entity, bool saveChanges = false, CancellationToken cancellationToken = default);
    Task Update(T entity, bool saveChanges = false, CancellationToken cancellationToken = default);
    Task Delete(T entity, bool saveChanges = false, CancellationToken cancellationToken = default);
    Task<int> SaveChanges(CancellationToken cancellationToken = default);
}
