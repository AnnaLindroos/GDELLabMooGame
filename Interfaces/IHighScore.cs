using LabMooGameG.Models;

namespace LabMooGameG.Interfaces;

public interface IHighScore
{
    void GetPlayerResults();
    void SortHighScoreResults();
    List<Player> GetHighScoreBoard();
}
