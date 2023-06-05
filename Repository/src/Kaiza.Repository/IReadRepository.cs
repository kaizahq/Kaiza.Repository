﻿namespace Kaiza.Repository;

public interface IReadRepository<T, TId> where T : class
{
    Task<T?> GetById(TId id, CancellationToken ct = default);
    Task<T?> FirstOrDefault(ISpecification<T> specification, CancellationToken ct = default);
    Task<T?> SingleOrDefault(ISpecification<T> specification, CancellationToken ct = default);
    Task<List<T>> List(ISpecification<T> specification, CancellationToken ct = default);
    Task<int> Count(ISpecification<T> specification, CancellationToken ct = default);
    Task<bool> Any(ISpecification<T> specification, CancellationToken ct = default);

}