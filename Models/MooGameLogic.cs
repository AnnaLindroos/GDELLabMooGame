using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;
public class MooGameLogic : IGameLogic
{
    private int _goalLength;
    private const int MAXCharacters = 4;
    private string _winningSequenceShuffled;
    // all acceptable characters for my string. Shuffle string and take the first four. Guarantees no duplicates and faster than the while loop. 
    // if I would accidentally pass in a long length this would never run, it would break if the MAX length is too high. 
    private string _winningSequenceString = "0123456789";
    // by not being strict about how long the goal length should be, I have the availability to add new game modes in the future with different lengths. 
    public MooGameLogic(int goalLength)
    {
        if (goalLength is <= 0 or > 10)
        {
            throw new ArgumentOutOfRangeException(nameof(goalLength), goalLength, "input must be between 1 and 10");
        }
        _goalLength = goalLength; 
    }
    public string GenerateWinningSequence()
    {
        Random randomNumbers = new Random();
        var shuffled = _winningSequenceString.OrderBy(x => randomNumbers.Next());
        var substring = shuffled.Take(_goalLength);
        _winningSequenceShuffled = new string (substring.ToArray());
        return _winningSequenceShuffled;
    }

    public string GenerateHint(string userGuess)
    {
        int bulls = 0, cows = 0;
        //Added input padding to ensure the guess has at least four characters.
        userGuess = userGuess.PadRight(MAXCharacters);

        for (int i = 0; i < MAXCharacters; i++)
        {
            for (int j = 0; j < MAXCharacters; j++)
            {
                if (_winningSequenceShuffled[i] == userGuess[j])
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
}
