using Microsoft.EntityFrameworkCore;
using NexignTest.Domain;

namespace NexignTest.Infrastructure.Persistence;

internal sealed class AppDbContext : DbContext
{
    public DbSet<DbPlayer> Players { get; set; } = null!;
    public DbSet<DbGame> Games { get; set; } = null!;
    public DbSet<DbRound> Rounds { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<DbPlayer>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
        });
        
        modelBuilder.Entity<DbGame>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatorId);
            builder.Property(x => x.MaxRoundsCount);
            builder
                .HasOne(x => x.Creator)
                .WithMany(x => x.CreatorGames)
                .HasForeignKey(x => x.CreatorId);
            builder
                .HasOne(x => x.Opponent)
                .WithMany(x => x.OpponentGames)
                .HasForeignKey(x => x.OpponentId);
        });

        modelBuilder.Entity<DbRound>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.Game)
                .WithMany(x => x.Rounds)
                .HasForeignKey(x => x.GameId);
        });
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        // Handling domain events (dirty, but example ;))
        var domainEntities = ChangeTracker
            .Entries<IAggregate>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToArray();

        foreach (var @event in domainEvents)
        {
            // Notify clients about some events (for example, game over)
        }
        
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}