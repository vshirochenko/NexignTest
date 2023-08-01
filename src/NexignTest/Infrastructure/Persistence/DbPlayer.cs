namespace NexignTest.Infrastructure.Persistence;

internal sealed class DbPlayer
{
    public Guid Id { get; }
    public string Name { get; }
    
    public List<DbGame> CreatorGames { get; } = new();
    public List<DbGame> OpponentGames { get; } = new();

    public DbPlayer(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}