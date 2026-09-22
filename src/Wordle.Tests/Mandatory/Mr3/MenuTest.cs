using Wordle.App;
using Wordle.App.ConsoleUI;

namespace Wordle.Tests.Mandatory.Mr3;

/// <summary>Обязательные тесты: меню и режим автопроверки.</summary>
public class MenuTest
{
    [Fact(DisplayName = "Некорректный пункт меню не роняет программу")]
    public void InvalidMenuChoiceDoesNotCrash()
    {
        var config = new GameConfig();
        var controller = new GameController(config);
        var renderer = new FakeRenderer();

        var input = new FakeInput(ConsoleKey.D5, ConsoleKey.D2);

        var gameLoop = new GameLoop(controller, renderer, input);
        var exception = Record.Exception(() => gameLoop.Run());

        Assert.Null(exception);
    }

    [Fact(DisplayName = "Можно сыграть несколько партий подряд без перезапуска")]
    public void SeveralGamesInARow()
    {
        var config = new GameConfig { DeterminedSeed = 67 };
        var controller = new GameController(config);

        for (var i = 0; i < 3; i++)
        {
            var session = controller.CreateNewGame();

            var guess = new Guess(config);
            guess.SetWord(session.Answer);

            controller.ApplyGuess(session, guess);
            Assert.Equal(session.Status, GameStatus.Win);
        }
    }

    [Fact(DisplayName = "Детерминированный режим даёт предсказуемый вывод для автопроверки")]
    public void DeterministicModeProducesPredictableOutput()
    {
        var config = new GameConfig { DeterminedSeed = 67 };

        var firstController = new GameController(config);
        var secondController = new GameController(config);

        for (var i = 0; i < 5; i++)
        {
            var firstSession = firstController.CreateNewGame();
            var secondSession = secondController.CreateNewGame();

            Assert.Equal(firstSession.Answer, secondSession.Answer);
        }
    }
}