using Game.Abstractions;

namespace Game.Tests.Mock;

class MockWordLoader(string[] words) : IWordLoader
{
    public string[] LoadWords(Action<string> callback)
    {
        return words;
    }
}