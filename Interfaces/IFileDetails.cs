namespace LabMooGameG.Interfaces;

public interface IFileDetails
{
    string GetFilePath();
    void MakeGameResultsFile(string userName, int numberOfGuesses);
}
