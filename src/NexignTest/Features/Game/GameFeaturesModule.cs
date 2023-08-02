namespace NexignTest.Features.Game;

internal static class GameFeaturesModule
{
    public static void MapGameEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/games", CreateGameFeature.Execute);
        builder.MapPost("/api/games/{gameId}/join", JoinGameFeature.Execute);
        builder.MapPost("/api/games/{gameId}/rounds", CreateRoundFeature.Execute);
        builder.MapPut("/api/games/{gameId}/rounds/{roundId}/turn", MakeTurnFeature.Execute);
    }
}