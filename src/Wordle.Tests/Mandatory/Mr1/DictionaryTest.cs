using Wordle.App;
using Wordle.App.ConsoleUI;

namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: словарь.</summary>
public class DictionaryTest
{
    [Fact(DisplayName = "Словарь содержит не меньше 50 слов")]
    public void DictionaryContainsAtLeastFiftyWords()
    {
        var config = new GameConfig();
        var dictionary = config.Words;

        Assert.True(dictionary.Count >= config.WordsCount);
    }

    [Fact(DisplayName = "Все слова словаря состоят ровно из 5 букв")]
    public void AllWordsAreExactlyFiveLettersLong()
    {
        var config = new GameConfig();
        var dictionary = config.Words;
        var requiredWordLength = 5;

       Assert.All(dictionary.Values, word => Assert.Equal(requiredWordLength, word.Length));
    }

    [Fact(DisplayName = "Пустой словарь приводит к ошибке, а не к запуску игры без слова")]
    public void EmptyDictionaryIsRejected()
    {
        Assert.Throws<ArgumentException>(() =>
            {
                var config = new GameConfig
                {
                    Words = new Dictionary<int, string>()
                };

                var controller = new GameController(config);
                var renderer = new GameRenderer();
                var input = new ConsoleInput();

                var gameLoop = new GameLoop(controller, renderer, input);

                gameLoop.Run();
            }
        );
    }
}