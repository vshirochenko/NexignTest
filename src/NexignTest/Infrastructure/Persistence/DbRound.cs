namespace NexignTest.Infrastructure.Persistence;

internal sealed class DbRound
{
    public Guid Id { get; }
    public int Number { get; set; }
    public int? CreatorTurn { get; set; }
    public int? OpponentTurn { get; set; }

    public Guid GameId { get; set; }
    public DbGame Game { get; set; } = null!;

    public DbRound(Guid id)
    {
        Id = id;
    }
}