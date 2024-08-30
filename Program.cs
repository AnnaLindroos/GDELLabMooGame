using LabMooGameG.Models;
using LabMooGameG.Interfaces;
using LabMooGameG.UI;
using LabMooGameG.MooGame;
using LabMooGameG.Data;

namespace MooGameG;

class Program
{
    public static void Main(string[] args)
    {
        IIO userIO = new ConsoleIO();

        IDataContext dataContext = new DataContext(DataContextConfigCreator.CreateConfig());

        Controller controller = new Controller(userIO, dataContext);
        controller.RunProgram();
    }
}

