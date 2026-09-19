namespace Wordle.App;

public class Guess
{
    private readonly WordDictionary _wordDictionary;

    private readonly HashSet<char> _correctCharacters = new() 
    {
        'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 
        'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 
        'х', 'ц', 'ч', 'ш','щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
    };

    public string Word { get; private set; }

    public Guess(string word, WordDictionary wordDictionary)
    {
        ChangeWord(word);
        _wordDictionary = wordDictionary;
    }

    public bool IsValid(out List<string> messages)
    {
        var isValid = true;
        messages = new List<string>();

        if (string.IsNullOrEmpty(Word))
        {
            messages.Add($"Слово не может быть пустым ((");
            return false;
        }

        if (Word.Length != _wordDictionary.WordLength)
        {
            messages.Add($"Длина слова должна быть {_wordDictionary.WordLength} символов");
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

        if (!_wordDictionary.Words.ContainsValue(Word))
        {
            messages.Add($"Слова не существует");
            isValid = false;
        }

        return isValid;
    }

    public void ChangeWord(string word)
    {
        if (!string.IsNullOrEmpty(word))
            Word = word.ToLowerInvariant().Trim();
        else Word = string.Empty;
    }
}
