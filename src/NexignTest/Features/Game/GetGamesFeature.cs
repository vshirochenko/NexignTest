using Microsoft.EntityFrameworkCore;
using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Features.Game;

internal static class GetGamesFeature
{
    public static async Task<IResult> Execute(
        AppDbContext db,
        CancellationToken stoppingToken)
    {
        var games = await db.Games.Select(x => new GameInListDto(x.Id)).ToArrayAsync(stoppingToken);
        return Results.Ok(games);
    }

    internal sealed record GameInListDto(Guid Id); 
}