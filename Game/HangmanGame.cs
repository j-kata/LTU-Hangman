namespace Game;

internal class HangmanGame
{
    private const char DefaultSymbol = '_';
    private string TargetWord { get; init; }
    public char[] HiddenWord { get; private set; }
    private HashSet<string> WrongGuesses { get; } = [];

    public HangmanGame(string word)
    {
        TargetWord = word.ToLower();
        HiddenWord = new string(DefaultSymbol, word.Length).ToCharArray();
    }

    public bool IsWordGuessed => TargetWord.SequenceEqual(HiddenWord);

    public bool MakeUniqueGuess(char letter)
    {
        var guess = letter.ToString().ToLower();
        if (HasAlreadyGuessed(guess)) return false;

        if (TargetWord.Contains(letter))
            RevealLetter(letter);
        else
            WrongGuesses.Add(guess);

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

    private bool HasAlreadyGuessed(string word)
    {
        return WrongGuesses.Contains(word)
                || HiddenWord.Contains(word.FirstOrDefault())
                || IsWordGuessed;
    }

    private void RevealLetter(char letter)
    {
        for (var i = 0; i < TargetWord.Length; i++)
            if (TargetWord[i] == letter)
                HiddenWord[i] = letter;
    }

    private void RevealWord() => HiddenWord = TargetWord.ToCharArray();
}