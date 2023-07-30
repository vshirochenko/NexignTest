using Microsoft.EntityFrameworkCore;
using NexignTest.Data;

namespace NexignTest.Features.Game;

internal static class JoinGameFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        JoinGameRequest req,
        IGameRepository gameRepository,
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var game = await gameRepository.Load(gameId, stoppingToken);
        game.Join(req.OpponentId);
        await gameRepository.Save(game, stoppingToken);
        return Results.Ok();
    } 
    
    internal sealed record JoinGameRequest(Guid OpponentId);
}