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

    public string GetFilePath()
    {
        return _filePath;
    }
}
