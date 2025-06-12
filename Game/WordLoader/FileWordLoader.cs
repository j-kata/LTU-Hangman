using Game.Abstractions;

namespace Game.WordLoader;

public class FileWordLoader(string filename) : IWordLoader
{
    public string[] LoadWords(Action<string> callback)
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
            callback("Exception: " + e.Message);
            return [];
        }
    }
}
