using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;
public class MooGameGoalGenerator : IGoalGenerator
{
    private int _goalLength;
    // all acceptable characters for my string. Shuffle string and take the first four. Guarantees no duplicates and faster than the while loop. 
    // if I woiuld accidentally pass in a long length this would never run, it would break if the MAX length is too high. 
    private string _winningSequenceString = "0123456789";

    // by not being strict about how long the goal length should be, I have the availability to add new game modes in the future with different lengths. 
    public MooGameGoalGenerator(int goalLength)
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
        return new string (substring.ToArray());
    }
}
