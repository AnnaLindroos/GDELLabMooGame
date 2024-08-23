using LabMooGameG.Interfaces;

namespace LabMooGameG.Models;

public class MooGameHighScore : IHighScore
{
    private IIO _userIO;
    private List<MooGamePlayer> _results;
    private IFileDetails _mooFileDetails;

    public MooGameHighScore(IFileDetails mooFileDetails, IIO userIO)
    {
        ArgumentNullException.ThrowIfNull(mooFileDetails);
        ArgumentNullException.ThrowIfNull(userIO);
        _userIO = userIO;
        _mooFileDetails = mooFileDetails;
    }

    public void GetPlayerResults()
    {
        try
        {
            _results = ReadPlayerDataFromFile();
        }
        catch (Exception e)
        {
            _userIO.Write($"Error retrieving player results from file: {e.Message}");
        }
    }

    private List<MooGamePlayer> ReadPlayerDataFromFile()
    {
        List<MooGamePlayer> results = new List<MooGamePlayer>();
        try
        {
            using (StreamReader input = new StreamReader(_mooFileDetails.GetFilePath()))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    ProcessPlayerData(line, results);
                }
            }
        }
        // Här samlas dom exceptions som StreamReader kan ge som relateras till FILEN jag försöker läsa ifrån.
        catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException or IOException)
        {
            _userIO.Write($"Error finding file, error finding directory or the file is busy: {e.Message}");
            throw;
        }
        return results;
    }


    public void ProcessPlayerData(string line, List<MooGamePlayer> results)
    {
        try
        {
            string[] playerNameAndScore = SplitPlayerNameAndScore(line);
            string playerName = playerNameAndScore[0];
            int guesses = Convert.ToInt32(playerNameAndScore[1]);

            UpdatePlayerResults(results, playerName, guesses);
        }
        // De vanligaste exceptions som kan uppstå när vi hanterar strängformattering
        catch (Exception e) when (e is FormatException or IndexOutOfRangeException)
        {
            _userIO.Write($"Error processing line '{line}': {e.Message}");
        }
    }

    private string[] SplitPlayerNameAndScore(string line)
    {
        return line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    }

    private void UpdatePlayerResults(List<MooGamePlayer> results, string playerName, int guesses)
    {
        MooGamePlayer playerData = new MooGamePlayer(playerName, guesses);

        MooGamePlayer playerExists = results.Find(x => x.PlayerName == playerName);

        if (playerExists == null)
        {
            AddNewPlayerResults(playerData, results);
        }
        else
        {
            playerExists.UpdatePlayerHighScore(guesses);
        }
    }

    public void AddNewPlayerResults(MooGamePlayer playerData, List<MooGamePlayer> results)
    {
        results.Add(playerData);
    }

    public void DisplayHighScoreBoard()
    {
        try
        {
            SortHighScoreResults();

            _userIO.Write("Player   games average");

            foreach (MooGamePlayer player in _results)
            {
                _userIO.Write($"{player.PlayerName,-9}{player.NumberOfGames,5}{player.GetAverageGuesses(),9:F2}");
            }
        }
        catch (Exception e)
        {
            _userIO.Write($"Error displaying high score board: {e.Message}");
        }
    }

    public void SortHighScoreResults()
    {
        _results = _results.OrderBy(p => p.GetAverageGuesses()).ToList();
    }
}
