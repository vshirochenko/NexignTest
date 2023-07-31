namespace NexignTest.Infrastructure.Persistence;

internal sealed class DbUser
{
    public Guid Id { get; }
    public string Name { get; }
    public List<DbGamePlayer> GamePlayers { get; } = new();

    public DbUser(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}