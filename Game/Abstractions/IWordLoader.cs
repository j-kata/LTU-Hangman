namespace Game.Abstractions;

public interface IWordLoader
{
    public string[] LoadWords(Action<string> callback);
}