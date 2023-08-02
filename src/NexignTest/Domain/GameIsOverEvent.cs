namespace NexignTest.Domain;

internal sealed class GameIsOverEvent : IDomainEvent
{
    public Guid GameId { get; }
    public Guid? WinnerId { get; }

    public GameIsOverEvent(Guid gameId, Guid? winnerId)
    {
        GameId = gameId;
        WinnerId = winnerId;
    }
}