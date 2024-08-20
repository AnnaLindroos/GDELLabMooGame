using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameG.Interfaces;

public interface IGameController
{
    void PlayGame();
    void StartNewGame(string userName);
    void PlayRound();
    string GetUserGuess();
    bool UserWantsToContinue();
}
