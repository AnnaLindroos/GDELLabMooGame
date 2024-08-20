using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGameG.Interfaces;

public interface IFileDetails
{
    string GetFilePath();
    void MakeGameResultsFile(string userName, int numberOfGuesses);
}
