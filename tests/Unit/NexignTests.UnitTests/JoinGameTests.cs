using FluentAssertions;
using NexignTest.Domain;
using Xunit;

namespace NexignTests.UnitTests;

public sealed class JoinGameTests
{
    [Fact]
    public void Third_player_should_not_be_able_to_join_game_with_two_already_joined_players()
    {
        // arrange
        var game = Game.Create(Guid.NewGuid(), Guid.NewGuid());
        game.Join(Guid.NewGuid());

        // act
        var act = () => game.Join(Guid.NewGuid());

        // assert
        act.Should().Throw<InvalidOperationException>();
    }
}