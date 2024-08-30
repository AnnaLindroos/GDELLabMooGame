using LabMooGameG.Data;
using LabMooGameG.Models;

namespace MooTests;

[TestClass]
public class DataContextTests
{
    private DataContext _dataContext;
    private List<Player> _players;

    [TestInitialize]
    public void Setup()
    {
        _dataContext = new DataContext(new DataContextConfig("testMooGame"));
        _dataContext.CreateFile("Anna", 2);
        _players = new List<Player>();
    }

    [TestMethod]
    public void DataContextTest()
    {
        Assert.IsNotNull(_dataContext);
    }

    [TestMethod]
    public void GetFilePathTest()
    {
        Assert.AreEqual("testMooGame", _dataContext.GetFilePath());
    }

    [TestMethod]
    public void CheckIfPlayerExistsUpdateTest()
    {
        string playerName = "Anna";
        int guesses = 2;

        _dataContext.CheckIfPlayerExists(_players, "Anna", 2);
        Assert.AreEqual(1, _players.Count);
        Assert.AreEqual(playerName, _players[0].PlayerName);
        Assert.AreEqual(guesses, _players[0].GuessesInTotal);
    }

    [TestMethod]
    public void CheckIfPlayerExistsAddTest()
    {
        string playerName = "Anna";
        int guesses = 5;
        int newGuesses = 2;
        Player existingPlayer = new Player(playerName, guesses);
        _players.Add(existingPlayer);

        _dataContext.CheckIfPlayerExists(_players, playerName, newGuesses);
        Assert.AreEqual(1, _players.Count());
        Assert.AreEqual(7, _players[0].GuessesInTotal);
    }
}
