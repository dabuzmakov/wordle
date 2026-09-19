namespace Wordle.App;

public class GameSession
{
    public int MaxAttempts { get; init; }
    public string Answer { get; init; }
    public string[] History { get; init; }
    public GameStatus Status { get; private set; }
    public int UsedAttempts { get; private set; }

    public GameSession(string answer, int maxAttempts)
    {
        if (maxAttempts <= 0)
            throw new ArgumentOutOfRangeException($"Количество попыток должно быть целым положительным числом");

        MaxAttempts = maxAttempts;
        Answer = answer;
        History = new string[MaxAttempts];
        Status = GameStatus.InProgress;
    }

    public void IncrementAttempts()
    {
        if (UsedAttempts >= MaxAttempts)
            throw new InvalidOperationException("Лимит попыток исчерпан.");

        UsedAttempts++;
    }
}
