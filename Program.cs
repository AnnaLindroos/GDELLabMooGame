using LabMooGameG.Models;
using LabMooGameG.Interfaces;
using LabMooGameG.Controllers;

namespace MooGameG;

class Program
{
    public static void Main(string[] args)
    {
        const int MAX = 4;
        IIO userIO = new ConsoleIO();
        IGameLogic mooGameLogic = new MooGameLogic(MAX);
        IFileDetails mooFileDetails = new MooGameFileDetails(userIO);
        IHighScore mooGameHighScore = new MooGameHighScore(mooFileDetails, userIO);

        MooGameController mooGameController = new MooGameController(userIO, mooGameLogic, mooGameHighScore, mooFileDetails);
        mooGameController.PlayGame();
    }
}

