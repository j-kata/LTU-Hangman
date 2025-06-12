using Game.Helpers;
using Game.UI;

namespace Game;

class HandmanSession(string[] words, IUI ui)
{
    private readonly Randomizer<string> _randomizer = new(words);

    public void Run()
    {
        bool keepPlaying = true;

        while (keepPlaying)
        {
            var manager = new HandmanManager(_randomizer.GetNextItem(), attempts: 10, ui);
            manager.RunRound();

            ui.PrintLine("Do you want to play again? (y/n) ");
            keepPlaying = ui.ReadLine()?.ToLower() == "y";
        }
    }
}