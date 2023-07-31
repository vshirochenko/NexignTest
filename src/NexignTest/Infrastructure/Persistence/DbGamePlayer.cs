namespace NexignTest.Infrastructure.Persistence;

internal sealed class DbGamePlayer
{
    public Guid GameId { get; set; }
    public DbGame Game { get; set; } = null!;
    
    public Guid PlayerId { get; set; }
    public DbUser Player { get; set; } = null!;
}