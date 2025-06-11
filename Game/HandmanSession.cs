using System.Text;
using Game.Helpers;

namespace Game;

class HandmanSession(string[] words)
{
    private readonly Randomizer<string> _randomizer = new(words);

    public void Run()
    {
        bool keepPlaying = true;

        while (keepPlaying)
        {
            var manager = new HandmanManager(_randomizer.GetNextItem(), attempts: 10);
            manager.RunRound();

            Console.Write("Do you want to play again? (y/n) ");
            keepPlaying = Console.ReadLine()?.ToLower() == "y";
        }
    }
}