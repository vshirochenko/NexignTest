using NexignTest.Infrastructure.Persistence;

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
        
        // TODO: check opponent existance!
        
        game.Join(req.OpponentId);
        await gameRepository.Save(game, stoppingToken);
        return Results.Ok();
    } 
    
    internal sealed record JoinGameRequest(Guid OpponentId);
}