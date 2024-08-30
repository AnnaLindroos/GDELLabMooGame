namespace LabMooGameG.Models;

public class Player 
{
    public string PlayerName { get; private set; }

    public int NumberOfGames { get; private set; }

    public int GuessesInTotal { get; private set; }

    public Player(string playerName, int guesses)
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
