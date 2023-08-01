namespace NexignTest.Features.Game;

internal static class CreateGameFeature
{
    public static async Task<IResult> Execute(
        CreateGameRequest req,
        IGameRepository gameRepository,
        CancellationToken stoppingToken)
    {
        var newGameId = Guid.NewGuid();
        var game = Domain.Game.Create(newGameId, req.CreatorId);
        await gameRepository.Save(game, stoppingToken);
        return Results.Created($"/api/games/{newGameId}", new CreateGameResponse(newGameId));
    } 
    
    internal sealed record CreateGameRequest(Guid CreatorId);

    internal sealed record CreateGameResponse(Guid Id); 
}