using Game.Abstractions;
using Game.UI;

namespace Game;

public class HangmanSession(
    IRandomizer<string> randomizer,
    Func<string, int, IUI, IManager> factory,
    IUI ui,
    int attempts = 10)
{
    public void Run()
    {
        bool keepPlaying = true;

        while (keepPlaying)
        {
            var manager = factory(randomizer.GetNextItem(), attempts, ui);
            manager.RunRound();

            ui.Print("Do you want to play again? (y/n) ");
            keepPlaying = ui.ReadLine()?.ToLower() == "y";
        }
    }
}