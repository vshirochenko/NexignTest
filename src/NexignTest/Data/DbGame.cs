namespace NexignTest.Data;

internal sealed class DbGame
{
    public Guid Id { get; }
    public Guid CreatorId { get; } // TODO: foreign key
    public List<DbGamePlayer> GamePlayers { get; } = new();

    public DbGame(Guid id, Guid creatorId)
    {
        Id = id;
        CreatorId = creatorId;
    }
}