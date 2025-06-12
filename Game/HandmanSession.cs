using Game.Abstractions;
using Game.UI;

namespace Game;

class HandmanSession(IRandomizer<string> randomizer, Func<string, int, IUI, IManager> factory, IUI ui)
{
    public void Run()
    {
        bool keepPlaying = true;

        while (keepPlaying)
        {
            var manager = factory(randomizer.GetNextItem(), 10, ui);
            manager.RunRound();

            ui.PrintLine("Do you want to play again? (y/n) ");
            keepPlaying = ui.ReadLine()?.ToLower() == "y";
        }
    }
}