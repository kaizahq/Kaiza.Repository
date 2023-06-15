using Kaiza.Repository.EntityFrameworkCore.Evaluators;
using Kaiza.Repository.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Kaiza.Repository.EntityFrameworkCore;

public abstract class Repository<T> : IRepository<T>
    where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ISpecificationEvaluator _specificationEvaluator;

    public Repository(DbContext context)
        : this(context, SpecificationEvaluator.Default) { }

    public Repository(DbContext context, ISpecificationEvaluator specificationEvaluator)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _specificationEvaluator = specificationEvaluator;
    }

    public virtual async Task<T?> GetById<TId>(TId id, CancellationToken ct = default)
        where TId : notnull
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken: ct);
    }

    public virtual async Task<T?> FirstOrDefault(
        ISpecification<T> specification,
        CancellationToken ct = default
    )
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(ct);
    }

    public virtual async Task<T?> SingleOrDefault(
        ISpecification<T> specification,
        CancellationToken ct = default
    )
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(ct);
    }

    public virtual async Task<List<T>> List(
        ISpecification<T> specification,
        CancellationToken ct = default
    )
    {
        return await ApplySpecification(specification).ToListAsync(ct);
    }

    public virtual async Task<int> Count(
        ISpecification<T> specification,
        CancellationToken ct = default
    )
    {
        return await ApplySpecification(specification, true).CountAsync(ct);
    }

    public virtual async Task<bool> Any(
        ISpecification<T> specification,
        CancellationToken ct = default
    )
    {
        return await ApplySpecification(specification, true).AnyAsync(ct);
    }

    public virtual async Task Add(
        T entity,
        bool saveChanges = false,
        CancellationToken ct = default
    )
    {
        _dbSet.Add(entity);

        if (saveChanges)
            await _context.SaveChangesAsync(ct);
    }

    public virtual async Task Update(
        T entity,
        bool saveChanges = false,
        CancellationToken ct = default
    )
    {
        _dbSet.Update(entity);

        if (saveChanges)
            await _context.SaveChangesAsync(ct);
    }

    public virtual async Task Delete(
        T entity,
        bool saveChanges = false,
        CancellationToken ct = default
    )
    {
        _dbSet.Remove(entity);

        if (saveChanges)
            await _context.SaveChangesAsync(ct);
    }

    public virtual async Task<int> SaveChanges(CancellationToken ct = default)
    {
        return await _context.SaveChangesAsync(ct);
    }

    protected virtual IQueryable<T> ApplySpecification(
        ISpecification<T> specification,
        bool evaluateCriteriaOnly = false
    )
    {
        return _specificationEvaluator.GetQuery(
            _dbSet.AsQueryable(),
            specification,
            evaluateCriteriaOnly
        );
    }
}
