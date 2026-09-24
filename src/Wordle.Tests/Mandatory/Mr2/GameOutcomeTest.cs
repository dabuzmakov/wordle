using Wordle.App;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: завершение партии.</summary>
public class GameOutcomeTest
{
    private readonly GameConfig _config = new GameConfig();

    [Fact(DisplayName = "Угаданное слово переводит сессию в статус WIN")]
    public void CorrectGuessWinsTheGame()
    {
        var controller = new GameController(_config);
        var session = controller.CreateNewGame();

        var guess = new Guess(_config);
        guess.SetWord(session.Answer);

        controller.ApplyGuess(session, guess);

        Assert.Equal(GameStatus.Win, session.Status);
    }

    [Fact(DisplayName = "После 6 неудачных попыток сессия переходит в статус LOSE")]
    public void SixFailedAttemptsLoseTheGame()
    {
        var controller = new GameController(_config);
        var session = controller.CreateNewGame();

        var seed = 123123213;
        var wordSelector = new WordSelector(_config.Words, seed);

        for (var i = 0; i < 6; i++)
        {
            var word = wordSelector.GetRandomWord();

            while (word == session.Answer)
                word = wordSelector.GetRandomWord();

            var guess = new Guess(_config);
            guess.SetWord(word);

            controller.ApplyGuess(session, guess);
        }

        Assert.Equal(GameStatus.Lose, session.Status);
    }

    [Fact(DisplayName = "При поражении показывается загаданное слово")]
    public void AnswerIsRevealedOnLoss()
    {
        var answer = _config.Words.First().Value;
        var controller = new GameController(_config);
        var session = new GameSession(answer, _config.MaxAttempts);

        var seed = 321324234;
        var wordSelector = new WordSelector(_config.Words, seed);

        for (var i = 0; i < 6; i++)
        {
            var word = wordSelector.GetRandomWord();

            while (word == session.Answer)
                word = wordSelector.GetRandomWord();

            var guess = new Guess(_config);
            guess.SetWord(word);

            controller.ApplyGuess(session, guess);
        }

        Assert.Equal(GameStatus.Lose, session.Status);
        Assert.Equal(answer, session.Answer);
    }

    [Fact(DisplayName = "Завершённая партия больше не принимает попытки")]
    public void FinishedGameRejectsFurtherGuesses()
    {
        var controller = new GameController(_config);
        var session = controller.CreateNewGame();

        var guess = new Guess(_config);
        guess.SetWord(session.Answer);

        controller.ApplyGuess(session, guess);

        Assert.Throws<InvalidOperationException>(() => controller.ApplyGuess(session, guess));
    }
}