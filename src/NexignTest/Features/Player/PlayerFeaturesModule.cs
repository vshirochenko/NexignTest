namespace NexignTest.Features.Player;

internal static class PlayerFeaturesModule
{
    public static void MapPlayerEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/players", CreatePlayerFeature.Execute);
        builder.MapGet("/api/players", GetPlayersFeature.Execute);
    }
}