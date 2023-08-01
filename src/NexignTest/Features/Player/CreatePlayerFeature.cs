using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Features.Player;

internal static class CreatePlayerFeature
{
    public static async Task<IResult> Execute(
        CreatePlayerRequest req,
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var newPlayerId = Guid.NewGuid();
        ctx.Players.Add(new DbPlayer(newPlayerId, req.Name));
        await ctx.SaveChangesAsync(stoppingToken);
        return Results.Created($"/api/players/{newPlayerId}", new CreatePlayerResponse(newPlayerId, req.Name));
    } 
    
    internal sealed record CreatePlayerRequest(string Name);

    internal sealed record CreatePlayerResponse(Guid Id, string Name); 
}