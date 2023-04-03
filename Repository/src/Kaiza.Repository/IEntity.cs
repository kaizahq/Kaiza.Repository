 namespace Kaiza.Repository;

public interface IEntity<TId>
{
    TId Id { get; set; }
}