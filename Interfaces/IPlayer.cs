using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameG.Interfaces;

public interface IPlayer
{
    public string PlayerName { get; set; }
    public int NumberOfGames { get; set; }
    public int GuessesInTotal { get; set; }
    void UpdatePlayerHighScore(int guesses);
    double GetAverageGuesses();
}
