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
            dbGame.MaxRoundsCount,
            dbGame.OpponentId, 
            dbGame.Rounds.Select(x => Round.Load(x.Id, x.Number, (TurnKind?) x.CreatorTurn, (TurnKind?) x.OpponentTurn)).OrderBy(x => x.Number).ToList());
        return game;
    }

    public async Task Save(Game game, CancellationToken stoppingToken = default)
    {
        var dbGame = await _db.Games.FindAsync(game.Id, stoppingToken);
        if (dbGame is null)
        {
            _db.Games.Add(new DbGame(game.Id, game.CreatorId, game.MaxRoundsCount));
        }
        else
        {
            dbGame.OpponentId = game.OpponentId;
            
            foreach (var round in game.Rounds)
            {
                // TODO: подумать про то, стоит ли заморачиваться с предыдущими раундами,
                // они все-таки readonly...
                var dbRound = dbGame.Rounds.SingleOrDefault(x => x.Id == round.Id); 
                if (dbRound is null)
                {
                    _db.Rounds.Add(new DbRound(round.Id)
                    {
                        GameId = game.Id, 
                        Number = round.Number, 
                        CreatorTurn = (int?)round.CreatorTurn,
                        OpponentTurn = (int?)round.OpponentTurn
                    });
                }
                else
                {
                    dbRound.Number = round.Number;
                    dbRound.CreatorTurn = (int?) round.CreatorTurn;
                    dbRound.OpponentTurn = (int?) round.OpponentTurn;
                }
            }
        }
        await _db.SaveChangesAsync(stoppingToken);
    }
}