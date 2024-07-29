using LabMooGame.Models;
using LabMooGame;
using LabMooGame.Interfaces;
using System.Runtime.InteropServices;
using LabMooGame.Controllers;

namespace MooGame;

class Program
{
    public static void Main(string[] args)
    {
        const int MAX = 4;

        IIO userIO = new ConsoleIO();
        IGoalGenerator goalGenerator = new MooGameGoalGenerator(MAX);
        IFileDetails mooFileDetails = new MooGameFileDetails();
        IHighScore mooGameHighScore = new MooGameHighScore(mooFileDetails, userIO);
        MooGameController mooGameController = new(userIO, goalGenerator, mooGameHighScore, mooFileDetails);
        mooGameController.PlayGame();
    }
}

