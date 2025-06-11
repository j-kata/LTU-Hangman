namespace Game;

internal class HangmanGame(string word, int attempts)
{
    private int Attempts { get; set; } = attempts;
    private HiddenWord HiddenWord { get; } = new HiddenWord(word);
    private WrongGuesses WrongGuesses { get; } = new();

    public int AttemptsLeft => Attempts;
    public string Word => HiddenWord.GuessedWord;
    public string Guesses => WrongGuesses.ToString();

    public void MakeGuess(string guess)
    {
        if (IsGameOver() || string.IsNullOrWhiteSpace(guess)) return;

        guess = guess.Trim().ToLowerInvariant();

        if (guess.Length == 1)
            MakeLetterGuess(guess[0]);
        else
            MakeWordGuess(guess);
    }

    private void MakeLetterGuess(char letter)
    {
        if (!HiddenWord.TryMakeUniqueGuess(letter, out bool correct))
            return;

        if (!correct && !WrongGuesses.Add(letter.ToString()))
            return;

        Attempts--;
    }

    private void MakeWordGuess(string word)
    {
        if (!HiddenWord.TryMakeUniqueGuess(word, out bool correct))
            return;

        if (!correct && !WrongGuesses.Add(word))
            return;

        Attempts--;
    }

    public bool IsWon() => HiddenWord.IsWordGuessed;
    public bool IsGameOver() => Attempts <= 0 || IsWon();
}