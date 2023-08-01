namespace NexignTest.Infrastructure.Persistence;

internal sealed class DbGame
{
    public Guid Id { get; }
    public Guid CreatorId { get; }
    public DbPlayer Creator { get; set; } = null!;
    
    public Guid? OpponentId { get; set; }
    public DbPlayer? Opponent { get; set; }
    
    public List<DbRound> Rounds { get; } = new();

    public DbGame(Guid id, Guid creatorId)
    {
        Id = id;
        CreatorId = creatorId;
    }
}