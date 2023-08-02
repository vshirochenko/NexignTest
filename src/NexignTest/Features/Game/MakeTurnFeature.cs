using NexignTest.Domain;

namespace NexignTest.Features.Game;

internal static class MakeTurnFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        Guid roundId,
        MakeTurnRequest req,
        IGameRepository gameRepository,
        CancellationToken stoppingToken)
    {
        var game = await gameRepository.Load(gameId, stoppingToken);
        var result = game.MakeTurn(req.PlayerId, (TurnKind) req.Turn);
        await gameRepository.Save(game, stoppingToken);
        return Results.Ok(new MakeTurnResponse((int) result));
    } 
    
    internal sealed record MakeTurnRequest(Guid PlayerId, int Turn);

    internal sealed record MakeTurnResponse(int Result);
}