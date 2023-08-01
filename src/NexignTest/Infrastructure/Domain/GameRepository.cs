using Microsoft.EntityFrameworkCore;
using NexignTest.Domain;
using NexignTest.Features.Game;
using NexignTest.Infrastructure.Persistence;

namespace NexignTest.Infrastructure.Domain;

internal sealed class GameRepository : IGameRepository
{
    private readonly AppDbContext _db;

    public GameRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Game> Load(Guid id, CancellationToken stoppingToken = default)
    {
        var dbGame = await _db.Games
            .Include(x => x.Creator)
            .Include(x => x.Opponent)
            .Include(x => x.Rounds)
            .SingleAsync(x => x.Id == id, stoppingToken);
        
        var game = Game.Load(
            dbGame.Id, 
            dbGame.CreatorId, 
            dbGame.OpponentId, 
            dbGame.Rounds.Select(x => Round.Load(x.Id, x.Number, (TurnKind?) x.CreatorTurn, (TurnKind?) x.OpponentTurn)).ToList());
        return game;
    }

    public async Task Save(Game game, CancellationToken stoppingToken = default)
    {
        var dbGame = await _db.Games.FindAsync(game.Id, stoppingToken);
        if (dbGame is null)
        {
            _db.Games.Add(new DbGame(game.Id, game.CreatorId));
        }
        else
        {
            dbGame.OpponentId = game.OpponentId;
            
            foreach (var round in game.Rounds)
            {
                if (!dbGame.Rounds.Any(x => x.Id == round.Id))
                {
                    _db.Rounds.Add(new DbRound(round.Id)
                    {
                        GameId = game.Id, 
                        Number = round.Number, 
                        CreatorTurn = (int?)round.CreatorTurn,
                        OpponentTurn = (int?)round.OpponentTurn
                    });
                }
                
            }
        }
        await _db.SaveChangesAsync(stoppingToken);
    }
}