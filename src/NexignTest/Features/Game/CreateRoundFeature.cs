namespace NexignTest.Features.Game;

internal static class CreateRoundFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        IGameRepository gameRepository,
        CancellationToken stoppingToken)
    {
        var game = await gameRepository.Load(gameId, stoppingToken);
        game.StartNewRound();
        await gameRepository.Save(game, stoppingToken);
        return Results.Ok();
    }
}