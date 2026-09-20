namespace Wordle.App;

public class Guess
{
    private readonly GameConfig _config;

    private readonly HashSet<char> _correctCharacters = new() 
    {
        'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 
        'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 
        'х', 'ц', 'ч', 'ш','щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
    };

    public string Word { get; private set; }

    public Guess(GameConfig config)
    {
        _config = config;
    }

    public bool IsValid(out List<string> messages)
    {
        var isValid = true;
        messages = new List<string>();

        if (string.IsNullOrEmpty(Word))
        {
            messages.Add($"Слово не может быть пустым");
            return false;
        }

        if (Word.Length != _config.WordLength)
        {
            messages.Add($"Длина слова должна быть {_config.WordLength} символов");
            isValid = false;
        }

        foreach (var sym in Word)
        {
            if (!_correctCharacters.Contains(sym))
            {
                messages.Add($"Слово должно содержать буквы русского алфавита");
                isValid = false;
                break;
            }
        }

        if (!_config.Words.ContainsValue(Word))
        {
            messages.Add($"Слово не существует");
            isValid = false;
        }

        return isValid;
    }

    public void SetWord(string word)
    {
        if (!string.IsNullOrEmpty(word))
            Word = word.ToLowerInvariant().Trim();
        else Word = string.Empty;
    }
}
