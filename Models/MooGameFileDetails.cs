using LabMooGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabMooGame.Models;

public class MooGameFileDetails : IFileDetails
{
    // So that no one can access it and change it throughout the class 
    private readonly string _filePath = "hellomoogameresult.txt";
    private IIO _userIO;

    public MooGameFileDetails(IIO userIO)
    {
        _userIO = userIO;
    }

    public string GetFilePath()
    {
        return _filePath;
    }

    public void MakeGameResultsFile(string userName, int numberOfGuesses)
    {
        try
        {
            using (StreamWriter output = new StreamWriter(_filePath, append: true))
            {
                output.WriteLine($"{userName}#&#{numberOfGuesses}");
            }
        }
        catch (Exception e) when (e is DirectoryNotFoundException or IOException)
        {
            _userIO.Write($"Error finding file, error finding directory or the file is busy: {e.Message}");
            throw;
        }
    }
}
