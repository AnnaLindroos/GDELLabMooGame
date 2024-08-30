using LabMooGameG.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace MooTests;

[TestClass]
public class ConsoleIOTests
{
   [TestMethod]
    public void ReadTest()
    {
        var input = "test input";
        var consoleIO = new ConsoleIO();

        using (var stringReader = new StringReader(input))
        {
            Console.SetIn(stringReader);

            var result = consoleIO.Read();

            Assert.AreEqual(input, result);
        }
    }

    [TestMethod]
    public void WriteLineTest()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        var consoleIO = new ConsoleIO();
        var message = "Hello, World!";

        consoleIO.WriteLine(message);

        Assert.AreEqual($"{message}{Environment.NewLine}", output.ToString());
    }
}
