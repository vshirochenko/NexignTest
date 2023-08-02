namespace NexignTest.Domain;

public interface IAggregate
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
}