using Microsoft.EntityFrameworkCore;
using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Features.Player;

internal static class GetPlayersFeature
{
    public static async Task<IResult> Execute(
        AppDbContext ctx,
        CancellationToken stoppingToken)
    {
        var players = await ctx.Players.Select(x => new PlayerInListDto(x.Id, x.Name)).ToArrayAsync(stoppingToken);
        return Results.Ok(players);
    }
    
    internal sealed record PlayerInListDto(Guid Id, string Name);
}