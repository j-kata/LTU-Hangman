namespace Game.Helpers;

internal class Randomizer<T>
{
    private int _currentIndex = 0;
    private readonly Random Random = new();
    public T[] Items { get; init; }

    public Randomizer(T[] items)
    {
        Items = [.. items];
        Random.Shuffle(Items);
    }

    public T GetNextItem()
    {
        if (_currentIndex >= Items.Length)
        {
            Random.Shuffle(Items);
            _currentIndex = 0;
        }
        return Items[_currentIndex++];
    }
}
