using System.Linq.Expressions;

namespace Kaiza.Repository.Expressions;

public record WhereExpression<T>(Expression<Func<T, bool>> Filter) where T : class;