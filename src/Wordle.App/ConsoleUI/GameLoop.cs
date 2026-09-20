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
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    var session = _controller.CreateNewGame();
                    ProcessGame(session);
                    break;

                case ConsoleKey.D2:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    public void ProcessGame(GameSession session)
    {
        while (session.Status == GameStatus.InProgress)
        {
            var guess = new Guess(Console.ReadLine(), _controller.Config);

            while (!guess.IsValid(out var messages))
            {
                guess.ChangeWord(Console.ReadLine());
            }

            var result = _controller.ApplyGuess(session, guess);

            
        }
    }
}
