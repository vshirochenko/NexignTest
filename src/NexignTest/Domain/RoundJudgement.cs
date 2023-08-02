namespace NexignTest.Domain;

public static class RoundJudgement
{
    public static RoundWinner GetWinner(TurnKind firstPlayerTurn, TurnKind secondPlayerTurn)
        => (firstTurn: firstPlayerTurn, secondTurn: secondPlayerTurn) switch
        {
            // Первый игрок показывает "камень"
            (TurnKind.Rock, TurnKind.Rock) => RoundWinner.Draw,
            (TurnKind.Rock, TurnKind.Scissors) => RoundWinner.FirstPlayer,
            (TurnKind.Rock, TurnKind.Paper) => RoundWinner.SecondPlayer,

            // Первый игрок показывает "ножницы"
            (TurnKind.Scissors, TurnKind.Rock) => RoundWinner.SecondPlayer,
            (TurnKind.Scissors, TurnKind.Scissors) => RoundWinner.Draw,
            (TurnKind.Scissors, TurnKind.Paper) => RoundWinner.FirstPlayer,

            // Первый игрок показывает "бумагу"
            (TurnKind.Paper, TurnKind.Rock) => RoundWinner.FirstPlayer,
            (TurnKind.Paper, TurnKind.Scissors) => RoundWinner.SecondPlayer,
            (TurnKind.Paper, TurnKind.Paper) => RoundWinner.Draw,

            (_, _) => throw new ArgumentOutOfRangeException("Unrecognized move kind", (Exception?) null)
        };
}