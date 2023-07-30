using Microsoft.EntityFrameworkCore;
using NexignTest.Data;

namespace NexignTest.Features.User;

internal static class GetUsersFeature
{
    public static async Task<IResult> Execute(
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var users = await ctx.Users.Select(x => new UserInListDto(x.Id, x.Name)).ToArrayAsync(stoppingToken);
        return Results.Ok(users);
    }
    
    internal sealed record UserInListDto(Guid Id, string Name);
}