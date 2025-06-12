using Game.WordLoader;

namespace Game.Tests.Mock;

class MockWordLoader(string[] words) : IWordLoader
{
    public string[] LoadWords(Action<string> callback)
    {
        return words;
    }
}