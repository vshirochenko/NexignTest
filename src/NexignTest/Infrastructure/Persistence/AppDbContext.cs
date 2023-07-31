using Microsoft.EntityFrameworkCore;

namespace NexignTest.Infrastructure.Persistence;

internal sealed class AppDbContext : DbContext
{
    public DbSet<DbUser> Users { get; set; } = null!;
    public DbSet<DbGame> Games { get; set; } = null!;
    public DbSet<DbGamePlayer> GamePlayers { get; set; } = null!;
    public DbSet<DbRound> Rounds { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<DbUser>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
        });
        
        modelBuilder.Entity<DbGame>(builder =>
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatorId);
        });
        
        modelBuilder.Entity<DbGamePlayer>(builder =>
        {
            builder.HasKey(x => new { x.GameId, x.PlayerId });
            builder
                .HasOne(x => x.Game)
                .WithMany(x => x.GamePlayers)
                .HasForeignKey(x => x.GameId);
            builder
                .HasOne(x => x.Player)
                .WithMany(x => x.GamePlayers)
                .HasForeignKey(x => x.PlayerId);
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
}