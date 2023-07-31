using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Features.User;

internal static class CreateUserFeature
{
    public static async Task<IResult> Execute(
        CreateUserRequest req,
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var newUserId = Guid.NewGuid();
        ctx.Users.Add(new DbUser(newUserId, req.Name));
        await ctx.SaveChangesAsync(stoppingToken);
        return Results.Created($"/api/users/{newUserId}", new CreateUserResponse(newUserId, req.Name));
    } 
    
    internal sealed record CreateUserRequest(string Name);

    internal sealed record CreateUserResponse(Guid Id, string Name); 
}