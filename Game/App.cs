using Game.Abstractions;
using Game.Helpers;
using Game.UI;
using Game.WordLoader;

namespace Game;

public class App(string filePath)
{
    public void Run()
    {
        ConsoleUI ui = new();
        var words = LoadWords(ui);

        if (words.Length == 0)
        {
            ui.PrintLine("No words were loaded. Please check the file and try again.");
            return;
        }

        RunSession(words, ui);
    }

    private static Randomizer<string> InitRandomizer(string[] words) => new(words);

    private static Func<string, int, IUI, IManager> InitFactory()
    {
        return (word, attempts, ui) => new HandmanManager(word, attempts, ui);
    }

    private string[] LoadWords(IUI ui)
    {
        FileWordLoader wordLoader = new(filePath);
        return wordLoader.LoadWords(ui.PrintLine);
    }

    private static void RunSession(string[] words, IUI ui)
    {
        var randomizer = InitRandomizer(words);
        var factory = InitFactory();
        var session = new HandmanSession(randomizer, factory, ui);

        session.Run();
    }
}