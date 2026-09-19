using Wordle.App;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: раскраска букв.</summary>
public class LetterMatchingTest
{
    private readonly WordDictionary _words = new WordDictionary(5);
    private readonly GameConfig _config = new GameConfig();

    [Fact(DisplayName = "Базовый случай: загадано \"озеро\", ввод \"арбуз\" -> ❌🟡❌❌🟡")]
    public void BasicCase()
    {
        var controller = new GameController(_config, _words);
        var session = new GameSession("озеро", _config.MaxAttempts);
        var guess = new Guess("арбуз", _words);

        var result = controller.ApplyGuess(session, guess);

        Assert.Equal(
            new[]
            {
                LetterResult.Red,
                LetterResult.Yellow,
                LetterResult.Red,
                LetterResult.Red,
                LetterResult.Yellow
            },
            result.Colors);
    }

    [Fact(DisplayName = "Полное совпадение: загадано \"озеро\", ввод \"озеро\" -> ✅✅✅✅✅")]
    public void ExactMatch()
    {
        var controller = new GameController(_config, _words);
        var session = new GameSession("озеро", _config.MaxAttempts);
        var guess = new Guess("озеро", _words);

        var result = controller.ApplyGuess(session, guess);

        Assert.All(result.Colors, color => Assert.Equal(LetterResult.Green, color));
    }

    [Fact(DisplayName = "Повторяющиеся буквы: загадано \"сорок\", ввод \"оооом\" -> ❌✅❌✅❌")]
    public void RepeatedLettersAreNotDoubleCounted()
    {
        var controller = new GameController(_config, _words);
        var session = new GameSession("сорок", _config.MaxAttempts);
        var guess = new Guess("оооом", _words);

        var result = controller.ApplyGuess(session, guess);

        Assert.Equal(
            new[]
            {
                LetterResult.Red,
                LetterResult.Green,
                LetterResult.Red,
                LetterResult.Green,
                LetterResult.Red
            },
            result.Colors);
    }

    [Fact(DisplayName = "Ни одна буква не подошла: все позиции ❌")]
    public void NoMatchingLetters()
    {
        var controller = new GameController(_config, _words);
        var session = new GameSession("озеро", _config.MaxAttempts);
        var guess = new Guess("акула", _words);

        var result = controller.ApplyGuess(session, guess);

        Assert.All(result.Colors, color => Assert.Equal(LetterResult.Red, color));
    }
}