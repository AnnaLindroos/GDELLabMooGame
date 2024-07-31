using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Interfaces;

public interface IGameLogic
{
    string GenerateWinningSequence();

    string GenerateHint(string userGuess);

    bool IsCorrectGuess(string hint);
}
