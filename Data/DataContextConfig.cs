namespace LabMooGameG.Data;

public class DataContextConfig
{
    public readonly string _filePathMooGame = "moohighscoresG";
    public DataContextConfig(string filePathMooGame)
    {
        ArgumentNullException.ThrowIfNull(filePathMooGame);
        _filePathMooGame = filePathMooGame;
    }
}
