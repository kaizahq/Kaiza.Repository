namespace Kaiza.Repository;

public interface ISpecification<T> where T : class
{
    SpecificationQuery<T> GetQuery();
}
