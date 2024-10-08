﻿using LabMooGameG.Models;
using LabMooGameG.Interfaces;

namespace LabMooGameG.Data;

public class DataContext : IDataContext
{
    private DataContextConfig _dataContextConfig;

    public DataContext(DataContextConfig dataContextConfig)
    {
        ArgumentNullException.ThrowIfNull(dataContextConfig);
        _dataContextConfig = dataContextConfig;
    }

    public void CreateFile(string userName, int numberOfGuesses)
    {
        string filePath = GetFilePath();
        try
        {
            using (StreamWriter output = new StreamWriter(filePath, append: true))
            {
                output.WriteLine($"{userName}#&#{numberOfGuesses}");
            }
        }
        catch (DirectoryNotFoundException e)
        {
            throw new($"Error finding file or error finding directory");
        }
        catch (IOException e)
        {
            throw new($"Error, the file is busy");
        }
        catch (Exception e)
        {
            throw new Exception($"Error in data access layer");
        }
    }

    public string GetFilePath()
    {
        return _dataContextConfig._filePathMooGame;
    }

    public List<Player> ReadPlayerDataFromFile()
    {
        string filePath = GetFilePath();

        List<Player> players = new List<Player>();
        try
        {
            using (StreamReader input = new StreamReader(filePath))
            {
                string line;
                while ((line = input.ReadLine()) != null)
                {
                    string[] playerNameAndScore = SplitPlayerNameAndScore(line);
                    string playerName = playerNameAndScore[0];
                    int guesses = Convert.ToInt32(playerNameAndScore[1]);

                    CheckIfPlayerExists(players, playerName, guesses);
                }
            }
        }
        catch (Exception e)
        {
            throw new($"Error reading file: {e.Message}");
        }
        return players;
    }

    public void CheckIfPlayerExists(List<Player> players, string playerName, int guesses)
    {
        Player playerExists = players.Find(x => x.PlayerName == playerName);

        if (playerExists == null)
        {
            Player newPlayer = new Player(playerName, guesses);
            players.Add(newPlayer);
        }
        else
        {
            playerExists.UpdatePlayerHighScore(guesses);
        }
    }

    private string[] SplitPlayerNameAndScore(string line)
    {
        return line.Split(new string[] { "#&#" }, StringSplitOptions.None);
    }
}