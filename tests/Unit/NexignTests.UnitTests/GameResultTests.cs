using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class GameResultTests
{
    [Fact]
    public void Creator_should_win_if_he_has_1_win_but_opponent_has_zero()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId, maxRoundsCount: 1);
        game.Join(opponentId);

        // act
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock);
        game.MakeTurn(opponentId, TurnKind.Scissors);

        // assert
        game.WinnerId.Should().Be(creatorId);
    }
    
    [Fact]
    public void Game_result_should_be_draw_if_both_players_has_1_win()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId, maxRoundsCount: 2);
        game.Join(opponentId);

        // act
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock); // Win
        game.MakeTurn(opponentId, TurnKind.Scissors); // Loose
        
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock); // Loose
        game.MakeTurn(opponentId, TurnKind.Paper); // Win

        // assert
        game.WinnerId.Should().Be(null);
    }

    [Fact]
    public void Creator_is_winner_before_all_rounds_completed_because_he_has_already_enough_round_wins()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId, maxRoundsCount: 3);
        game.Join(opponentId);
        
        // act
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock); // Win
        game.MakeTurn(opponentId, TurnKind.Scissors); // Loose
        
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock); // Win
        game.MakeTurn(opponentId, TurnKind.Scissors); // Loose
        
        // assert
        game.WinnerId.Should().Be(creatorId);
    }
}