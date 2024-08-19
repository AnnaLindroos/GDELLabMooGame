using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;

public class MooGamePlayer 
{
    public string PlayerName { get; private set; }

    public int NumberOfGames { get; private set; }

    public int GuessesInTotal { get; private set; }

    public MooGamePlayer(string playerName, int guesses)
    {
        PlayerName = playerName;
        NumberOfGames = 1;
        GuessesInTotal = guesses;
    }

    public void UpdatePlayerHighScore(int guesses)
    {
        GuessesInTotal += guesses;
        NumberOfGames++;
    }

    public double GetAverageGuesses()
    {
        return (double)GuessesInTotal / NumberOfGames;
    }
}
