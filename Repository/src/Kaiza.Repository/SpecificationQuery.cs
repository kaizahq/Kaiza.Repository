using Kaiza.Repository.Expressions;

namespace Kaiza.Repository;

public class SpecificationQuery<T> where T : class
{
    public bool AsNoTracking { get; set; }
    public bool AsSplitQuery { get; set; }
    public bool IgnoreQueryFilters { get; set; }
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public List<string> IncludeStrings { get; } = new List<string>();
    public List<WhereExpression<T>> WhereExpressions { get; } = new List<WhereExpression<T>>();

    public List<OrderExpression<T>> OrderExpressions { get; } = new List<OrderExpression<T>>();
}