namespace Game;

internal class WrongGuesses
{
    private HashSet<string> WrongGuesesSet { get; } = [];

    public bool Add(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return false;

        return WrongGuesesSet.Add(word.ToLowerInvariant());
    }

    public override string ToString()
    {
        return string.Join(", ", WrongGuesesSet);
    }
}