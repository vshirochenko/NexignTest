namespace NexignTest.Domain;

public sealed class Round
{
    public Guid Id { get; }
    public int Number { get; }
    public TurnKind? CreatorTurn { get; private set; }
    public TurnKind? OpponentTurn { get; private set; }

    private Round(Guid id, int number)
    {
        Id = id;
        Number = number;
    }

    public static Round Create(Guid id, int number)
    {
        return new Round(id, number);
    }

    public static Round Load(Guid id, int number, TurnKind? creatorTurn, TurnKind? opponentTurn)
    {
        var round = new Round(id, number)
        {
            CreatorTurn = creatorTurn,
            OpponentTurn = opponentTurn
        };
        return round;
    }

    public void MakeCreatorTurn(TurnKind turn)
    {
        CreatorTurn = turn;
    }
    
    public void MakeOpponentTurn(TurnKind turn)
    {
        OpponentTurn = turn;
    }
}