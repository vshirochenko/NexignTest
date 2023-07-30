using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public class JoinGameTests
{
    [Fact]
    public void Third_player_should_not_be_able_to_join_existing_game()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        game.Join(Guid.NewGuid()); // After that we have two players already - stop!

        // act
        var act = () => game.Join(Guid.NewGuid());

        // assert
        act.Should().Throw<InvalidOperationException>();
    }
}