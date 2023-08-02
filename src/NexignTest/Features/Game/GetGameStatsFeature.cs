using Microsoft.EntityFrameworkCore;
using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Features.Game;

internal static class GetGameStatsFeature
{
    public static async Task<IResult> Execute(
        Guid gameId,
        AppDbContext db,
        CancellationToken stoppingToken)
    {
        var game = await db.Games
            .Include(x => x.Creator)
            .Include(x => x.Opponent)
            .Include(x => x.Rounds)
            .SingleAsync(x => x.Id == gameId, stoppingToken);
        var stats = new GameStatsResponse(
            game.Id,
            game.CreatorId,
            game.OpponentId,
            game.MaxRoundsCount,
            game.IsOver,
            game.WinnerId,
            game.Rounds.Select(x => new RoundDto(x.Id, x.Number, x.CreatorTurn, x.OpponentTurn, x.Winner)).ToArray());
        return Results.Ok(stats);
    }

    private sealed record GameStatsResponse(Guid Id, Guid CreatorId, Guid? OpponentId, int MaxRoundsCount,
        bool IsOver, Guid? WinnerId, IReadOnlyCollection<RoundDto> Rounds);

    private sealed record RoundDto(Guid Id, int Number, int? CreatorTurn, int? OpponentTurn, int? Winner);
}