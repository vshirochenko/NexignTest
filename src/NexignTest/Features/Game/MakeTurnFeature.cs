using NexignTest.Domain;

namespace NexignTest.Features.Game;

internal static class MakeTurnFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        Guid roundId, // TODO: stats
        MakeTurnRequest req,
        IGameRepository gameRepository,
        CancellationToken stoppingToken)
    {
        var game = await gameRepository.Load(gameId, stoppingToken);
        game.MakeTurn(req.PlayerId, (TurnKind) req.Turn);
        await gameRepository.Save(game, stoppingToken);
        return Results.Ok();
    } 
    
    internal sealed record MakeTurnRequest(Guid PlayerId, int Turn);
}