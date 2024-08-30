using LabMooGameG.Interfaces;

namespace LabMooGameG.UI;

public class ConsoleIO : IIO
{
    public ConsoleIO() { }

    public string Read()
    {
        return Console.ReadLine() ?? "";
    }

    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
}
