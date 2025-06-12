using Game.Helpers;
using Game.WordLoader;

namespace Game;

class Program
{

    static void Main(string[] args)
    {
        FileWordLoader wordLoader = new (Environment.CurrentDirectory + "/Data/words.txt");
        string[] words = wordLoader.LoadWords();

        if (words.Length == 0)
        {
            Console.WriteLine("No words were loaded. Please check the file and try again.");
            return;
        }

        HandmanSession session = new(words);
        session.Run();
    }
}
