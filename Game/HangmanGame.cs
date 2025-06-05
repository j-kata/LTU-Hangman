namespace Game;

internal class HangmanGame
{
    private const char DefaultSymbol = '_';
    private string TargetWord { get; init; }
    private char[] HiddenLetters { get; set; }
    private HashSet<string> WrongGuesses { get; } = [];
    
    public string HiddenWord => string.Join("", HiddenLetters) ?? string.Empty;
    public string WrongGuessesDisplay => string.Join(", ", WrongGuesses);

    public HangmanGame(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) throw new ArgumentException(nameof(word));

        TargetWord = word.ToLower();
        HiddenLetters = new string(DefaultSymbol, word.Length).ToCharArray();
    }

    public bool IsWordGuessed => TargetWord.SequenceEqual(HiddenLetters);

    public bool MakeUniqueGuess(char letter)
    {
        var guess = char.ToLower(letter);
        if (HasAlreadyGuessed(guess)) return false;

        if (TargetWord.Contains(guess))
            RevealLetter(guess);
        else
            WrongGuesses.Add(guess.ToString());
        return true;
    }

    public bool MakeUniqueGuess(string word)
    {
        var guess = word.ToLower();
        if (HasAlreadyGuessed(guess)) return false;

        if (guess == TargetWord)
            RevealWord();
        else
            WrongGuesses.Add(word);

        return true;
    }

    private bool HasAlreadyGuessed(char letter)
    {
        return IsWordGuessed
            || WrongGuesses.Contains(letter.ToString())
            || HiddenLetters.Contains(letter);
    }


    private bool HasAlreadyGuessed(string word)
    {
        return IsWordGuessed || WrongGuesses.Contains(word);
    }

    private void RevealLetter(char letter)
    {
        for (var i = 0; i < TargetWord.Length; i++)
            if (TargetWord[i] == letter)
                HiddenLetters[i] = letter;
    }

    private void RevealWord() => HiddenLetters = TargetWord.ToCharArray();
}