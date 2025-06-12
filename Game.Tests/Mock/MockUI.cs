using Game.Abstractions;

namespace Game.Tests.Mock;

class MockUI : IUI
{

    private readonly Queue<string?> _inputs = new();
    private readonly List<string?> _outputs = new();
    public IReadOnlyList<string?> Outputs => _outputs;

    public void SetInput(string input) => _inputs.Enqueue(input.Trim());
    public void SetInputs(IEnumerable<string> inputs)
    {
        foreach (string item in inputs)
            SetInput(item);
    }

    public void Print(string? str) => _outputs.Add(str);
    public void PrintLine(string? str) => _outputs.Add(str + "\n");
    public string? ReadLine() => _inputs.Dequeue();
}
