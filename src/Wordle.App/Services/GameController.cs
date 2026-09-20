namespace Wordle.App;

public class GameController
{
    public GameConfig Config { get; init; }

    public GameController(GameConfig config)
    {
        config.Validate();
        Config = config;
    }

    public GuessResult ApplyGuess(GameSession session, Guess guess)
    {
        var result = new GuessResult(guess);
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
        var wordSelector = new WordSelector(Config.Words, seed);

        var answer = wordSelector.GetRandomWord();
        var session = new GameSession(answer, Config.MaxAttempts);

        return session;
    }
}
