namespace Game.WordLoader;

public class FileWordLoader(string filename) : IWordLoader
{
    public string[] LoadWords()
    {
        try
        {
            var content = File.ReadAllText(filename);
            return content
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.Trim())
                .ToArray();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
            return [];
        }
    }
}
