using Kaiza.Repository.EntityFrameworkCore.Evaluators;
using Kaiza.Repository.Evaluators;
using Microsoft.EntityFrameworkCore;

namespace Kaiza.Repository.EntityFrameworkCore;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;
    protected readonly ISpecificationEvaluator _specificationEvaluator;

    public Repository(DbContext context)
        : this(context, SpecificationEvaluator.Default)
    {
    }

    public Repository(DbContext context, ISpecificationEvaluator specificationEvaluator)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _specificationEvaluator = specificationEvaluator;
    }

    public async Task<T?> GetById<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
    {
        return await _dbSet.FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<T?> FirstOrDefault(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> SingleOrDefault(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> List(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification).ToListAsync(cancellationToken);
    }

    public async Task<int> Count(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification, true).CountAsync(cancellationToken);
    }

    public async Task<bool> Any(ISpecification<T> specification, CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
    }

    public async Task Add(T entity, bool saveChanges = false, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);

        if (saveChanges) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(T entity, bool saveChanges = false, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        if (saveChanges) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(T entity, bool saveChanges = false, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);

        if (saveChanges) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
    {
        return _specificationEvaluator.GetQuery(_dbSet.AsQueryable(), specification, evaluateCriteriaOnly);
    }
}
