using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class GameLoop
{
    private readonly GameController _controller;
    private readonly GameRenderer _renderer;

    public GameLoop(GameController controller, GameRenderer renderer)
    {
        _controller = controller;
        _renderer = renderer;
    }

    public void Run()
    {
        var maxAttempts = _controller.Config.MaxAttempts;
        var wordLength = _controller.Config.WordLength;
        _renderer.ShowHomeScreen(maxAttempts, wordLength);

        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    var session = _controller.CreateNewGame();
                    ProcessGame(session);
                    break;

                case ConsoleKey.D2:
                    ProcessExit();
                    break;
            }
        }
    }

    private void ProcessGame(GameSession session)
    {
        _renderer.Clear();
        _renderer.ShowBanner(ConsoleColor.Yellow, ConsoleBanner.LogoBanner);

        while (session.Status == GameStatus.InProgress)
        {
            _renderer.ShowInputBox(session.UsedAttempts, session.MaxAttempts);

            var guess = new Guess(_controller.Config);
            ProcessInput(guess);

            while (!guess.IsValid(out var messages))
            {
                _renderer.ShowErrors(messages);
                ProcessInput(guess);
            }

            _renderer.ClearErrors();

            var result = _controller.ApplyGuess(session, guess);
            _renderer.ShowGuessResult(result, session.UsedAttempts);
        }
    }

    private void ProcessInput(Guess guess)
    {
        var (left, top) = (_renderer.InputLeft, _renderer.InputTop);

        Console.SetCursorPosition(left, top);
        guess.SetWord(Console.ReadLine());

        Console.SetCursorPosition(left, top);
        Console.Write(new string(' ', 75));
    }

    private void ProcessExit()
    {
        _renderer.Clear();
        _renderer.ShowBanner(ConsoleColor.Red, ConsoleBanner.ExitBanner);
        Environment.Exit(0);
    }
}
