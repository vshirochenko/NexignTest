using System.Diagnostics.CodeAnalysis;

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

    public RoundResult MakeCreatorTurn(TurnKind turn)
    {
        CreatorTurn = turn;

        if (!HasOpponentMadeTurn()) 
            return RoundResult.NotReady;
        
        var winner = RoundJudgement.GetWinner(CreatorTurn.Value, OpponentTurn.Value);
        var result = winner switch
        {
            RoundWinner.FirstPlayer => RoundResult.Won,
            RoundWinner.SecondPlayer => RoundResult.Lost,
            RoundWinner.Draw => RoundResult.Draw,
            _ => throw new ArgumentOutOfRangeException("Unrecognized round winner", (Exception?) null)
        };

        return result;
    }
    
    public RoundResult MakeOpponentTurn(TurnKind turn)
    {
        OpponentTurn = turn;
        
        if (!HasCreatorMadeTurn()) 
            return RoundResult.NotReady;
        
        var winner = RoundJudgement.GetWinner(CreatorTurn.Value, OpponentTurn.Value);
        var result = winner switch
        {
            RoundWinner.FirstPlayer => RoundResult.Lost,
            RoundWinner.SecondPlayer => RoundResult.Won,
            RoundWinner.Draw => RoundResult.Draw,
            _ => throw new ArgumentOutOfRangeException("Unrecognized round winner", (Exception?) null)
        };

        return result;
    }

    [MemberNotNullWhen(returnValue: true, member: nameof(CreatorTurn))]
    public bool HasCreatorMadeTurn()
    {
        return CreatorTurn is not null;
    }
    
    [MemberNotNullWhen(returnValue: true, member: nameof(OpponentTurn))]
    public bool HasOpponentMadeTurn()
    {
        return OpponentTurn is not null;
    }
    
    public bool IsRoundOver()
    {
        return HasCreatorMadeTurn() && HasOpponentMadeTurn();
    }
}