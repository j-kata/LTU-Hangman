namespace Game.WordLoader;

public interface IWordLoader
{
    public string[] LoadWords(Action<string> callback);
}