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

public class MooGameController : IGame
{
    private const int MAXCharacters = 4;
    private IIO _userIO;
    private IGoalGenerator _goalGenerator;
    private IHighScore _mooGameHighScore;
    private IFileDetails _mooGameFileDetails;
    private string _winningSequence;
    private int _numberOfGuesses;

    public MooGameController(IIO userIO, IGoalGenerator goalGenerator, IHighScore mooGameHighScore, IFileDetails mooGameFileDetails)
    {
        // sexy ways to handle errors instead of doing if statements, no need for nameofs either 
        ArgumentNullException.ThrowIfNull(userIO);
        ArgumentNullException.ThrowIfNull(goalGenerator);
        ArgumentNullException.ThrowIfNull(mooGameHighScore);
        ArgumentNullException.ThrowIfNull(mooGameFileDetails);
        _userIO = userIO;
        _goalGenerator = goalGenerator;
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
        _winningSequence = _goalGenerator.GenerateWinningSequence();

        _userIO.Write("New game\n");
        _userIO.Write("For practice, number is: " + _winningSequence + "\n");
    }

    // Catches exceptions specific to the game round logic, such as errors in processing guesses.
    public void PlayRound()
    {
        while (true)
        {
            try
            {
                string userGuess = GetUserGuess();
                _numberOfGuesses++;
                string hint = GenerateHint(userGuess);

                _userIO.Write(hint + "\n");
                if (IsCorrectGuess(hint))
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

    public string GenerateHint(string userGuess)
    {
        int bulls = 0, cows = 0;
        userGuess = userGuess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_winningSequence[i] == userGuess[j])
                {
                    if (i == j)
                    {
                        bulls++;
                    }
                    else
                    {
                        cows++;
                    }
                }
            }
        }
        return new string('B', bulls) + "," + new string('C', cows);
    }

    public bool IsCorrectGuess(string hint)
    {
        return hint == "BBBB,";
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


