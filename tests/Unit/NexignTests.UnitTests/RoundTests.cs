using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class RoundTests
{
    [Fact]
    public void Cannot_start_more_than_five_rounds()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        for (var i = 0; i < 5; i++) 
            game.StartNewRound();

        // act
        var act = () => game.StartNewRound();
        
        // assert 
        act.Should().Throw<InvalidOperationException>();
    }
}