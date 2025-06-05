using Game.Helpers;

namespace Game.Tests;

public class RandomizerTests
{

    [Fact]
    public void GetNextItem_ReturnsAllItemsBerforRepeat()
    {
        var array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var randomizer = new Randomizer<int>(array);

        var seen = new HashSet<int>();
        for (int i = 0; i < array.Length; i++)
        {
            var item = randomizer.GetNextItem();
            seen.Add(item);
        }
        Assert.Equal(array.Length, seen.Count);
    }

    [Fact]
    public void GetNextItem_ReshufflesAfterExhausting()
    {
        var array = new int[10] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        var randomizer = new Randomizer<int>(array);

        var round1 = new List<int>();
        var round2 = new List<int>();

        for (int i = 0; i < array.Length; i++)
            round1.Add(randomizer.GetNextItem());

        for (int i = 0; i < array.Length; i++)
            round2.Add(randomizer.GetNextItem());

        // Can't rely on shuffle direct and check rounds for equality
        Assert.All(array, item => Assert.Contains(item, round1));
        Assert.All(array, item => Assert.Contains(item, round2));
    }    
}
