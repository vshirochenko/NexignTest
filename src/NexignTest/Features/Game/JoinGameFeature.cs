using NexignTest.Data;

namespace NexignTest.Features.Game;

internal static class JoinGameFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        JoinGameRequest req,
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var game = await ctx.Games.FindAsync(gameId, stoppingToken);
        if (game is null)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        var joiningPlayer = await ctx.Users.FindAsync(req.OpponentId, stoppingToken);
        if (joiningPlayer is null)
            return Results.StatusCode(StatusCodes.Status500InternalServerError);

        ctx.GamePlayers.Add(new DbGamePlayer { GameId = gameId, PlayerId = req.OpponentId });
        await ctx.SaveChangesAsync(stoppingToken);
        return Results.Ok();
    } 
    
    internal sealed record JoinGameRequest(Guid OpponentId);
}