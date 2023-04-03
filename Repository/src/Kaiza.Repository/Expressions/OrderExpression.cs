using System.Linq.Expressions;

namespace Kaiza.Repository.Expressions;

public enum OrderDirection
{
    Ascending = 1,
    Descending = 2
}

public record OrderExpression<T>(Expression<Func<T, object?>> KeySelector, OrderDirection Direction) where T : class;