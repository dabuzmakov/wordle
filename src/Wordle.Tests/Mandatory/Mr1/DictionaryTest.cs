using Wordle.App;

namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: словарь.</summary>
public class DictionaryTest
{
    [Fact(DisplayName = "Словарь содержит не меньше 50 слов")]
    public void DictionaryContainsAtLeastFiftyWords()
    {
        var dictionary = new WordDictionary(5);

        Assert.True(dictionary.Words.Count >= 50);
    }

    [Fact(DisplayName = "Все слова словаря состоят ровно из 5 букв")]
    public void AllWordsAreExactlyFiveLettersLong()
    {
        var dictionary = new WordDictionary(5);

        Assert.All(dictionary.Words.Values, word => Assert.Equal(5, word.Length));
    }

    [Fact(DisplayName = "Пустой словарь приводит к ошибке, а не к запуску игры без слова")]
    public void EmptyDictionaryIsRejected()
    {
        var emptyDictionary = new Dictionary<int, string>();
        var randomSeed = 342432;

        Assert.Throws<ArgumentException>(
            () => new WordSelector(emptyDictionary, randomSeed)
        );
    }
}