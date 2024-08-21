namespace LabMooGameG.Interfaces;

public interface IGameController
{
    void PlayGame();
    void StartNewGame(string userName);
    void PlayRound();
    string GetUserGuess();
    bool UserWantsToContinue();
}
