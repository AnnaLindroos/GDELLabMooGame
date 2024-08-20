using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameG.Interfaces;

public interface IHighScore
{
    void GetPlayerResults();
    void SortHighScoreResults();
    void DisplayHighScoreBoard();
}
