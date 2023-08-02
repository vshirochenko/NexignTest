using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class RoundTests
{
    [Fact]
    public void Cannot_start_first_round_if_second_opponent_has_not_joined_yet()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        
        // act
        var act = () => game.StartNewRound();
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Can_start_new_round_if_it_will_be_the_first()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        game.Join(Guid.NewGuid());
        
        // act
        var act = () => game.StartNewRound();
        
        // assert
        act.Should().NotThrow();
        game
            .Rounds.Should().ContainSingle().Which
            .Number.Should().Be(1);
    }
    
    [Fact]
    public void Cannot_start_new_round_if_current_round_is_not_completed()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        game.Join(Guid.NewGuid());
        game.StartNewRound();
        
        // act
        var act = () => game.StartNewRound();
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Cannot_start_more_than_five_rounds()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId);
        game.Join(opponentId);
        for (var i = 0; i < 5; i++)
        {
            // Imitate players' turns
            game.StartNewRound();
            game.MakeTurn(creatorId, TurnKind.Rock);
            game.MakeTurn(opponentId, TurnKind.Rock);
        }
            

        // act
        var act = () => game.StartNewRound();
        
        // assert 
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Unknown_player_from_outside_cannot_make_turn()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        game.Join(Guid.NewGuid());
        game.StartNewRound();
        var playerFromOutsideId = Guid.NewGuid(); 
        
        // act
        var act = () => game.MakeTurn(playerFromOutsideId, TurnKind.Paper);
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Turn_in_round_by_creator_should_be_recognized_as_game_creator_turn()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var creatorTurn = TurnKind.Rock;
        var game = Game.Create(Guid.NewGuid(), creatorId);
        game.Join(Guid.NewGuid());
        game.StartNewRound();
        
        // act
        game.MakeTurn(creatorId, creatorTurn);
        
        // assert
        game.CurrentRound.Should().NotBeNull();
        game.CurrentRound!.CreatorTurn.Should().Be(creatorTurn);
    }
    
    [Fact]
    public void Turn_in_round_by_opponent_should_be_recognized_as_opponent_turn()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        var opponentId = Guid.NewGuid();
        var opponentTurn = TurnKind.Paper;
        game.Join(opponentId);
        game.StartNewRound();
        
        // act
        game.MakeTurn(opponentId, opponentTurn);
        
        // assert
        game.CurrentRound.Should().NotBeNull();
        game.CurrentRound!.OpponentTurn.Should().Be(opponentTurn);
    }

    [Fact]
    public void Creator_cannot_make_turn_twice_in_the_same_round()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId);
        game.Join(Guid.NewGuid());
        game.StartNewRound();
        game.MakeTurn(creatorId, TurnKind.Rock);
        
        // act
        var act = () => game.MakeTurn(creatorId, TurnKind.Rock);
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void Opponent_cannot_make_turn_twice_in_the_same_round()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        var opponentId = Guid.NewGuid();
        game.Join(opponentId);
        game.StartNewRound();
        game.MakeTurn(opponentId, TurnKind.Rock);
        
        // act
        var act = () => game.MakeTurn(opponentId, TurnKind.Rock);
        
        // assert
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Result_should_not_be_ready_if_creator_made_turn_but_opponent_did_not()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId);
        game.Join(opponentId);
        game.StartNewRound();

        // act
        var result = game.MakeTurn(creatorId, TurnKind.Rock);

        // assert
        result.Should().Be(RoundResult.NotReady);
    }
    
    [Fact]
    public void Result_should_not_be_ready_if_opponent_made_turn_but_creator_did_not()
    {
        // arrange
        var creatorId = Guid.NewGuid();
        var opponentId = Guid.NewGuid();
        var game = Game.Create(Guid.NewGuid(), creatorId);
        game.Join(opponentId);
        game.StartNewRound();

        // act
        var result = game.MakeTurn(opponentId, TurnKind.Rock);

        // assert
        result.Should().Be(RoundResult.NotReady);
    }
}