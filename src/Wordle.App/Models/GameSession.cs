namespace Wordle.App;

public class GameSession
{
    public int MaxAttempts { get; init; }
    public string Answer { get; init; }
    public GuessResult[] History { get; init; }
    public GameStatus Status { get; private set; }
    public int UsedAttempts { get; private set; }

    public GameSession(string answer, int maxAttempts)
    {
        if (maxAttempts <= 0)
            throw new ArgumentOutOfRangeException($"Количество попыток должно быть целым положительным числом");

        MaxAttempts = maxAttempts;
        Answer = answer;
        History = new GuessResult[MaxAttempts];
        Status = GameStatus.InProgress;
    }

    public void RegisterAttempt(GuessResult result)
    {
        if (UsedAttempts >= MaxAttempts)
            throw new InvalidOperationException("Лимит попыток исчерпан.");

        if (Status != GameStatus.InProgress)
            throw new InvalidOperationException("Игра окончена");

        History[UsedAttempts] = result;
        UsedAttempts++;

        if (result.IsAllGreen())
            Status = GameStatus.Win;

        if (Status == GameStatus.InProgress 
            && UsedAttempts == MaxAttempts)
            Status = GameStatus.Lose;
    }
}
