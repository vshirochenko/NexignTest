using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class TurnsTests
{
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.Draw)]
    [InlineData(TurnKind.Scissors, RoundWinner.FirstPlayer)]
    [InlineData(TurnKind.Paper, RoundWinner.SecondPlayer)]
    public void Rock_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var playerTurn = TurnKind.Rock;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.SecondPlayer)]
    [InlineData(TurnKind.Scissors, RoundWinner.Draw)]
    [InlineData(TurnKind.Paper, RoundWinner.FirstPlayer)]
    public void Scissors_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var playerTurn = TurnKind.Scissors;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.FirstPlayer)]
    [InlineData(TurnKind.Scissors, RoundWinner.SecondPlayer)]
    [InlineData(TurnKind.Paper, RoundWinner.Draw)]
    public void Paper_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var playerTurn = TurnKind.Paper;

        // act
        var actualWinner = RoundJudgement.GetWinner(playerTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
}