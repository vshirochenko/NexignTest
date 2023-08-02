namespace NexignTest.Domain;

public static class RoundJudgement
{
    public static RoundWinner GetWinner(TurnKind creatorTurn, TurnKind opponentTurn)
        => (creatorTurn, opponentTurn) switch
        {
            // Первый игрок показывает "камень"
            (TurnKind.Rock, TurnKind.Rock) => RoundWinner.Draw,
            (TurnKind.Rock, TurnKind.Scissors) => RoundWinner.Creator,
            (TurnKind.Rock, TurnKind.Paper) => RoundWinner.Opponent,

            // Первый игрок показывает "ножницы"
            (TurnKind.Scissors, TurnKind.Rock) => RoundWinner.Opponent,
            (TurnKind.Scissors, TurnKind.Scissors) => RoundWinner.Draw,
            (TurnKind.Scissors, TurnKind.Paper) => RoundWinner.Creator,

            // Первый игрок показывает "бумагу"
            (TurnKind.Paper, TurnKind.Rock) => RoundWinner.Creator,
            (TurnKind.Paper, TurnKind.Scissors) => RoundWinner.Opponent,
            (TurnKind.Paper, TurnKind.Paper) => RoundWinner.Draw,

            (_, _) => throw new ArgumentOutOfRangeException("Unrecognized move kind", (Exception?) null)
        };
}