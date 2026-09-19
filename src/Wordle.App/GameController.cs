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
    }

    public GameResult ApplyGuess(GameSession session, string guess)
    {
        
    }

    public void StartNewGame()
    {
        var seed = new Random().Next();
        var wordSelector = new WordSelector(_wordDictionary.Words, seed);

        var answer = wordSelector.GetRandomWord();
        var session = new GameSession(answer, _config.MaxAttempts);

        while (session.Status == GameStatus.InProgress)
        {
            
        }
    }
}
