namespace NexignTest.Features.User;

internal static class UserFeaturesModule
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/users", CreateUserFeature.Execute);
        builder.MapGet("/api/users", GetUsersFeature.Execute);
    }
}