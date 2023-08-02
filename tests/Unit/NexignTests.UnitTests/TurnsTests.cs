using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class TurnsTests
{
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.Draw)]
    [InlineData(TurnKind.Scissors, RoundWinner.Creator)]
    [InlineData(TurnKind.Paper, RoundWinner.Opponent)]
    public void Rock_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var creatorTurn = TurnKind.Rock;

        // act
        var actualWinner = RoundJudgement.GetWinner(creatorTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.Opponent)]
    [InlineData(TurnKind.Scissors, RoundWinner.Draw)]
    [InlineData(TurnKind.Paper, RoundWinner.Creator)]
    public void Scissors_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var creatorTurn = TurnKind.Scissors;

        // act
        var actualWinner = RoundJudgement.GetWinner(creatorTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
    
    [Theory]
    [InlineData(TurnKind.Rock, RoundWinner.Creator)]
    [InlineData(TurnKind.Scissors, RoundWinner.Opponent)]
    [InlineData(TurnKind.Paper, RoundWinner.Draw)]
    public void Paper_turn_against_other_turns_should_produce_correct_result(TurnKind opponentTurn, RoundWinner expectedWinner)
    {
        // arrange
        var creatorTurn = TurnKind.Paper;

        // act
        var actualWinner = RoundJudgement.GetWinner(creatorTurn, opponentTurn);

        // assert
        actualWinner.Should().Be(expectedWinner);
    }
}