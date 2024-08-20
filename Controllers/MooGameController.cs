using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using LabMooGame.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LabMooGame.Interfaces;

namespace LabMooGame.Controllers;

public class MooGameController : IGameController
{
    private const int MAXCharacters = 4;
    private IIO _userIO;
    private IGameLogic _mooGameLogic;
    private IHighScore _mooGameHighScore;
    private IFileDetails _mooGameFileDetails;
    private string _winningSequence;
    private int _numberOfGuesses;

    public MooGameController(IIO userIO, IGameLogic mooGameLogic, IHighScore mooGameHighScore, IFileDetails mooGameFileDetails)
    {
        ArgumentNullException.ThrowIfNull(userIO);
        ArgumentNullException.ThrowIfNull(mooGameLogic);
        ArgumentNullException.ThrowIfNull(mooGameHighScore);
        ArgumentNullException.ThrowIfNull(mooGameFileDetails);
        _userIO = userIO;
        _mooGameLogic = mooGameLogic;
        _mooGameHighScore = mooGameHighScore;
        _mooGameFileDetails = mooGameFileDetails;
    }

    public void PlayGame()
    {
        _userIO.Write("Enter your user name:\n");
        string userName = _userIO.Read();

        bool playGame = true;

        while (playGame)
        {
            StartNewGame(userName);
            PlayRound();
            _mooGameFileDetails.MakeGameResultsFile(userName, _numberOfGuesses);
            _mooGameHighScore.GetPlayerResults();
            _mooGameHighScore.DisplayHighScoreBoard();

            _userIO.Write($"Correct, it took {_numberOfGuesses} guesses\nContinue?");

            if (!UserWantsToContinue())
            {
                playGame = false;
            }
        }
    }

    public void StartNewGame(string userName)
    {
        _numberOfGuesses = 0;
        _winningSequence = _mooGameLogic.GenerateWinningSequence();

        _userIO.Write("New game\n");
        _userIO.Write("For practice, number is: " + _winningSequence + "\n");
    }
    public void PlayRound()
    {
        while (true)
        {
            try
            {
                string userGuess = GetUserGuess();
                _numberOfGuesses++;
                string hint = _mooGameLogic.GenerateHint(userGuess);

                _userIO.Write(hint + "\n");
                if (_mooGameLogic.IsCorrectGuess(hint))
                {
                    break;
                }
            }
            catch (Exception e)
            {
                _userIO.Write($"Error processing your guess: {e.Message}\n");
            }
        }
    }

    public string GetUserGuess()
    {
        _userIO.Write("Enter your guess:\n");
        return _userIO.Read();
    }

    public bool UserWantsToContinue()
    {
        try
        {
            string response = _userIO.Read();
            return string.IsNullOrWhiteSpace(response) || response[0].ToString().ToLower() != "n";
        }
        catch (Exception e)
        {
            _userIO.Write($"Error reading your response: {e.Message}\n");
            return false;
        }
    }
}


