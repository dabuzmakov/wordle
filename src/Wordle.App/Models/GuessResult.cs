namespace Wordle.App;

public class GuessResult
{
    public string Word { get; init; }
    public LetterResult[] Colors { get; private set; }

    public GuessResult(Guess guess)
    {
        if (!guess.IsValid(out var messages))
            throw new ArgumentException(string.Join(", ", messages));

        Word = guess.Word;
        Colors = new LetterResult[Word.Length];
    }

    public bool IsAllGreen()
        => Colors.All(color => color == LetterResult.Green);
}
