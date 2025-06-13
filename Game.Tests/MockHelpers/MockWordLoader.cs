using Game.Abstractions;

namespace Game.Tests.MockHelpers;

class MockWordLoader(string[] words) : IWordLoader
{
    public string[] LoadWords(Action<string> callback)
    {
        return words;
    }
}