using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class MovesTests
{
    [Theory]
    [InlineData(MoveKind.Rock, RoundWinner.Draw)]
    [InlineData(MoveKind.Scissors, RoundWinner.FirstPlayer)]
    [InlineData(MoveKind.Paper, RoundWinner.SecondPlayer)]
    public void Rock_move_against_other_moves_should_produce_correct_result(MoveKind opponentMove, RoundWinner expectedWinner)
    {
        // arrange
        var playerMove = MoveKind.Rock;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerMove, opponentMove);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(MoveKind.Rock, RoundWinner.SecondPlayer)]
    [InlineData(MoveKind.Scissors, RoundWinner.Draw)]
    [InlineData(MoveKind.Paper, RoundWinner.FirstPlayer)]
    public void Scissors_move_against_other_moves_should_produce_correct_result(MoveKind opponentMove, RoundWinner expectedWinner)
    {
        // arrange
        var playerMove = MoveKind.Scissors;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerMove, opponentMove);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(MoveKind.Rock, RoundWinner.FirstPlayer)]
    [InlineData(MoveKind.Scissors, RoundWinner.SecondPlayer)]
    [InlineData(MoveKind.Paper, RoundWinner.Draw)]
    public void Paper_move_against_other_moves_should_produce_correct_result(MoveKind opponentMove, RoundWinner expectedWinner)
    {
        // arrange
        var playerMove = MoveKind.Paper;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerMove, opponentMove);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
}