namespace Wordle.App;

public class GameConfig
{
    public int MaxAttempts { get; init; } = 6;
    public int WordLength { get; init; } = 5;
    public int WordsCount { get; init; } = 50;
    public int? DeterminedSeed { get; init; } = null;

    public Dictionary<int, string> Words { get; init; } = new()
    {
        [0] = "арбуз",
        [1] = "банан",
        [2] = "берег",
        [3] = "билет",
        [4] = "бочка",
        [5] = "вагон",
        [6] = "ветер",
        [7] = "вилка",
        [8] = "волна",
        [9] = "город",
        [10] = "груша",
        [11] = "дверь",
        [12] = "дождь",
        [13] = "замок",
        [14] = "земля",
        [15] = "игрок",
        [16] = "кабан",
        [17] = "норка",
        [18] = "книга",
        [19] = "кошка",
        [20] = "лампа",
        [21] = "лодка",
        [22] = "маска",
        [23] = "метро",
        [24] = "мышка",
        [25] = "около",
        [26] = "отбор",
        [27] = "парус",
        [28] = "песок",
        [29] = "пилот",
        [30] = "пламя",
        [31] = "полка",
        [32] = "почта",
        [33] = "птица",
        [34] = "ручка",
        [35] = "самса",
        [36] = "сапог",
        [37] = "стена",
        [38] = "трава",
        [39] = "труба",
        [40] = "фрукт",
        [41] = "шапка",
        [42] = "шарик",
        [43] = "школа",
        [44] = "агент",
        [45] = "оооом",
        [46] = "акула",
        [47] = "океан",
        [48] = "озеро",
        [49] = "рыбак",
    };

    public void Validate()
    {
        if (WordLength <= 0)
            throw new ArgumentOutOfRangeException($"Длина слова должна быть целым положительным числом");

        if (Words == null || Words.Count == 0)
            throw new ArgumentException("Словарь не может быть пустым.");

        if (Words.Count() < WordsCount)
            throw new ArgumentException($"Количество слов должно быть не менее {WordsCount}");

        foreach (var word in Words.Values)
            if (word.Length != WordLength)
                throw new ArgumentException($"Слово '{word}' должно содержать {WordLength} букв.");
    }
}
