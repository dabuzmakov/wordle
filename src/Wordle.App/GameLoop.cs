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
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        _controller.StartNewGame();
                        break;

                    case ConsoleKey.D2:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
