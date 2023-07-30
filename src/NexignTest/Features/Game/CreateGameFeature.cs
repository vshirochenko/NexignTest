using NexignTest.Data;

namespace NexignTest.Features.Game;

internal static class CreateGameFeature
{
    public static async Task<IResult> Execute(
        CreateGameRequest req,
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var newGameId = Guid.NewGuid();
        ctx.Games.Add(new DbGame(newGameId, req.CreatorId));
        await ctx.SaveChangesAsync(stoppingToken);
        return Results.Created($"/api/users/{newGameId}", new CreateGameResponse(newGameId));
    } 
    
    internal sealed record CreateGameRequest(Guid CreatorId);

    internal sealed record CreateGameResponse(Guid Id); 
}