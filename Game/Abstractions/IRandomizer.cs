namespace Game.Abstractions;

public interface IRandomizer<T>
{
    T[] Items { get; init; }

    T GetNextItem();
}