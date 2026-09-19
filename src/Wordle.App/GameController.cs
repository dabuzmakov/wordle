namespace Wordle.App;

public class GameController
{
    private readonly GameConfig _config;
    private readonly WordDictionary _wordDictionary;
    private readonly GameRenderer _render;

    public GameController(GameConfig config, GameRenderer render)
    {
        _wordDictionary = new WordDictionary(config.WordLength);
        _render = render;
        _config = config;
    }

    public GuessResult ApplyGuess(GameSession session, Guess guess)
    {
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

    public void StartNewGame()
    {
        var seed = new Random().Next();
        var wordSelector = new WordSelector(_wordDictionary.Words, seed);

        var answer = wordSelector.GetRandomWord();
        var session = new GameSession(answer, _config.MaxAttempts);

        while (session.Status == GameStatus.InProgress)
        {
            var guess = new Guess(Console.ReadLine(), _config.WordLength);
            
            while (!guess.IsValid(out var messages))
            {
                guess.ChangeWord(Console.ReadLine());
            }

            var result = ApplyGuess(session, guess);
        }
    }
}
