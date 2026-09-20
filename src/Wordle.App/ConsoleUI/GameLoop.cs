using Wordle.App.ConsoleUI;

namespace Wordle.App;

public class GameLoop
{
    private readonly GameController _controller;
    private readonly IGameRenderer _renderer;
    private readonly IUserInput _input;

    public GameLoop(
        GameController controller, 
        IGameRenderer renderer, 
        IUserInput input)
    {
        _controller = controller;
        _renderer = renderer;
        _input = input;
    }

    public void Run()
    {
        var maxAttempts = _controller.Config.MaxAttempts;
        var wordLength = _controller.Config.WordLength;

        while (true)
        {
            _renderer.ShowHomeScreen(maxAttempts, wordLength);

            switch (_input.ReadKey(true))
            {
                case ConsoleKey.D1:
                    var session = _controller.CreateNewGame();
                    ProcessGame(session);
                    break;

                case ConsoleKey.D2:
                    _renderer.Clear();
                    _renderer.ShowBanner(ConsoleColor.Red, ConsoleBanner.ExitBanner);
                    return;
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

        if (session.Status == GameStatus.Win)
            _renderer.ShowBanner(ConsoleColor.Green, ConsoleBanner.WinBanner);
        else _renderer.ShowBanner(ConsoleColor.Red, ConsoleBanner.LoseBanner);

        _renderer.ShowContinueMessage();
        _input.ReadKey(true);
    }

    private void ProcessInput(Guess guess)
    {
        Console.CursorVisible = true;

        var (left, top) = (_renderer.InputLeft, _renderer.InputTop);

        Console.SetCursorPosition(left, top);
        guess.SetWord(Console.ReadLine());

        Console.SetCursorPosition(left, top);
        Console.Write(new string(' ', 75));

        Console.CursorVisible = false;
    }
}
