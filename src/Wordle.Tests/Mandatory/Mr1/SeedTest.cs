using Wordle.App;

namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: одинаковый seed обязан давать одинаковое загаданное слово.</summary>
public class SeedTest
{
    [Fact(DisplayName = "Одинаковый seed даёт одинаковое загаданное слово")]
    public void SameSeedProducesSameAnswer()
    {
        var dictionary = new WordDictionary(5);

        var selector1 = new WordSelector(dictionary.Words, 12345);
        var selector2 = new WordSelector(dictionary.Words, 12345);

        var word1 = selector1.GetRandomWord();
        var word2 = selector2.GetRandomWord();

        Assert.Equal(word1, word2);
    }

    [Fact(DisplayName = "Разные seed'ы дают разные слова хотя бы иногда")]
    public void DifferentSeedsProduceDifferentAnswers()
    {
        var dictionary = new WordDictionary(5);

        var seeds1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        var seeds2 = new int[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

        var isDifferent = false;

        for (var i = 0; i < 10; i++)
        {
            var selector1 = new WordSelector(dictionary.Words, seeds1[i]);
            var selector2 = new WordSelector(dictionary.Words, seeds2[i]);

            var word1 = selector1.GetRandomWord();
            var word2 = selector2.GetRandomWord();

            if (word1 != word2)
            {
                isDifferent = true;
                break;
            }
        }

        Assert.True(isDifferent);
    }
}