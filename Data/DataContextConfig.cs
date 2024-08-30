namespace LabMooGameG.Data;

public class DataContextConfig
{
    public readonly string _filePathMooGame = "moohighscores";
    public DataContextConfig(string filePathMooGame)
    {
        ArgumentNullException.ThrowIfNull(filePathMooGame);
        _filePathMooGame = filePathMooGame;
    }
}
