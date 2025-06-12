using Game.Helpers;
using Game.UI;
using Game.WordLoader;

namespace Game;

class Program
{
    static void Main(string[] args)
    {
        ConsoleUI ui = new();

        var filePath = $"{Environment.CurrentDirectory}/Data/words.txt";
        FileWordLoader wordLoader = new(filePath);
        string[] words = wordLoader.LoadWords(ui.PrintLine);

        if (words.Length == 0)
        {
            ui.PrintLine("No words were loaded. Please check the file and try again.");
            return;
        }

        var randomizer = new Randomizer<string>(words);
        var factory = (string word, int attempts, IUI ui) => new HandmanManager(word, attempts, ui);
        HandmanSession session = new(randomizer, factory, ui);

        session.Run();
    }
}
