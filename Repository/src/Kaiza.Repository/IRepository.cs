namespace Kaiza.Repository;

public interface IRepository<T, TId> : IReadRepository<T, TId> where T : class
{
    Task Add(T entity, bool saveChanges = false, CancellationToken ct = default);
    Task Update(T entity, bool saveChanges = false, CancellationToken ct = default);
    Task Delete(T entity, bool saveChanges = false, CancellationToken ct = default);
    Task<int> SaveChanges(CancellationToken ct = default);
}
