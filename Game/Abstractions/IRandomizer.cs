namespace Game.Abstractions;

internal interface IRandomizer<T>
{
    T[] Items { get; init; }

    T GetNextItem();
}