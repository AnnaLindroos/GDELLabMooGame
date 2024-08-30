using LabMooGameG.Models;

namespace LabMooGameG.Interfaces;

public interface IDataContext
{
    void CreateFile(string userName, int numberOfGuesses);
    string GetFilePath();
    List<Player> ReadPlayerDataFromFile();
    void CheckIfPlayerExists(List<Player> players, string playerName, int guesses);
}
