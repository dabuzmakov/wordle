using Wordle.App;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: валидация ввода.</summary>
public class GuessValidationTest
{
    private readonly GameConfig _config = new GameConfig();

    [Theory(DisplayName = "Слово не из 5 букв отклоняется")]
    [InlineData("дом")]
    [InlineData("домики")]
    [InlineData("")]
    public void WordOfWrongLengthIsRejected(string guess)
    {
        var invalidGuess = new Guess(_config);
        invalidGuess.SetWord(guess);

        Assert.False(invalidGuess.IsValid(out _));
    }

    [Theory(DisplayName = "Ввод с не-буквами отклоняется")]
    [InlineData("дом12")]
    [InlineData("дом!!")]
    [InlineData("до ма")]
    public void NonLetterInputIsRejected(string guess)
    {
        var invalidGuess = new Guess(_config);
        invalidGuess.SetWord(guess);

        Assert.False(invalidGuess.IsValid(out _));
    }

    [Fact(DisplayName = "Слово, которого нет в словаре, отклоняется")]
    public void WordOutsideDictionaryIsRejected()
    {
        var invalidGuess = new Guess(_config);
        invalidGuess.SetWord("щщщщщ");

        Assert.False(invalidGuess.IsValid(out _));
    }

    [Fact(DisplayName = "Некорректный ввод не тратит попытку")]
    public void InvalidInputDoesNotConsumeAttempt()
    {
        var controller = new GameController(_config);
        var session = controller.CreateNewGame();

        var invalidGuess = new Guess(_config);
        invalidGuess.SetWord("выа");

        Assert.Throws<ArgumentException>(
            () => controller.ApplyGuess(session, invalidGuess));

        Assert.Equal(0, session.UsedAttempts);
    }

    [Fact(DisplayName = "Ввод не зависит от регистра: \"ОЗЕРО\" и \"озеро\" обрабатываются одинаково")]
    public void InputIsCaseInsensitive()
    {
        var lower = "озеро";
        var upper = "ОЗЕРО";

        var lowerGuess = new Guess(_config);
        var upperGuess = new Guess(_config);

        lowerGuess.SetWord(lower);
        upperGuess.SetWord(upper);

        Assert.Equal(lowerGuess.Word, upperGuess.Word);
    }
}