namespace Wordle.App;

public class GameController
{
    private readonly GameConfig _config;
    private readonly WordDictionary _wordDictionary;

    public GameController(GameConfig config, WordDictionary wordDictionary)
    {
        _wordDictionary = wordDictionary;
        _config = config;
    }

    public GuessResult ApplyGuess(GameSession session, Guess guess)
    {
        if (!guess.IsValid(out var messages))
            throw new ArgumentException(string.Join(", ", messages));

        var result = new GuessResult(guess.Word);
        var remainingLetters = new Dictionary<char, int>();

        for (var i = 0; i < guess.Word.Length; i++)
        {
            var answerLetter = session.Answer[i];

            if (guess.Word[i] != answerLetter)
            {
                remainingLetters[answerLetter] = remainingLetters.GetValueOrDefault(answerLetter) + 1;
                result.Colors[i] = LetterResult.Red;
            }
            else result.Colors[i] = LetterResult.Green;
        }

        for (var i = 0; i < guess.Word.Length; i++)
        {
            if (result.Colors[i] == LetterResult.Green)
                continue;

            var letter = guess.Word[i];
            if (remainingLetters.TryGetValue(letter, out var count) && count > 0)
            {
                result.Colors[i] = LetterResult.Yellow;
                remainingLetters[letter]--;
            }
        }

        session.RegisterAttempt(result);
        return result;
    }

    public GameSession CreateNewGame()
    {
        var seed = new Random().Next();
        var wordSelector = new WordSelector(_wordDictionary.Words, seed);

        var answer = wordSelector.GetRandomWord();
        var session = new GameSession(answer, _config.MaxAttempts);

        return session;
    }
}
