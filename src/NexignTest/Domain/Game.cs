using System.Diagnostics.CodeAnalysis;

namespace NexignTest.Domain;

public sealed class Game
{
    public Guid Id { get; }
    public Guid CreatorId { get; }
    public Guid? OpponentId { get; private set; }
    
    private Game(Guid id, Guid creatorId)
    {
        Id = id;
        CreatorId = creatorId;
    }

    public static Game Create(Guid id, Guid creatorId)
    {
        return new Game(id, creatorId);
    }

    public void Join(Guid opponentId)
    {
        if (IsGameLobbyFull())
            throw new InvalidOperationException("Game lobby is full! Try another lobby :)");
        OpponentId = opponentId;
    }
    
    [MemberNotNullWhen(returnValue: true, member: nameof(OpponentId))]
    private bool IsGameLobbyFull() 
        => OpponentId is not null;
}