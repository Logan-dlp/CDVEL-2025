// Copyright 2024, Logan.dlp, All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SerializeScore
{
    private static string _filePath = $"{Application.persistentDataPath}/ScoreSave.json";
    
    public static int GetPlayerNumber()
    {
        if (GetAllScore() != null)
        {
            return GetAllScore().Count;
        }
        else
        {
            return 0;
        }
    }

    public static KeyValuePair<string, float> GetHightPlayer()
    {
        try
        {
            Dictionary<string, float> hightPlayer = GetAllScoreDescending().First().Value;
            return hightPlayer.First();
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"There is no registered player.\n{ex}");
            return new KeyValuePair<string, float>("none", 0);
        }
    }
    
    public static void AddScore(string name, float value)
    {
        Dictionary<int, Dictionary<string, float>> scoreToSave = new();
        
        if (File.Exists(_filePath) && GetAllScore() != null)
        {
            scoreToSave = GetAllScore();
        }

        Dictionary<string, float> newscore = new();
        newscore.Add(name, value);
        
        scoreToSave.Add(scoreToSave.Count+1, newscore);
        
        try
        {
            string json = JsonConvert.SerializeObject(scoreToSave, Formatting.Indented);

            using FileStream stream = new(_filePath, FileMode.Create);
            using StreamWriter writer = new(stream);
            writer.Write(json);
            
            Debug.Log("Save have been success.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Json Serialization failed\n{ex}");
        }
    }

    public static void ClearAllScore()
    {
        if (File.Exists(_filePath))
        {
            File.WriteAllText(_filePath,"{}");
        }
    }
    
    public static Dictionary<int, Dictionary<string, float>> GetAllScore()
    {
        try
        {
            using StreamReader reader = new(_filePath);
            string json = reader.ReadToEnd();

            Dictionary<int, Dictionary<string, float>> AllScore = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<string, float>>>(json);
            return AllScore;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Json deserialization failed\n{ex}");
            return null;
        }
    }

    public static Dictionary<int, Dictionary<string, float>> GetAllScoreAscending()
    {
        Dictionary<int, Dictionary<string, float>> originDictionary = GetAllScore();
        List<(int, string, float)> sortedList = new();

        foreach (KeyValuePair<int,Dictionary<string,float>> k in originDictionary)
        {
            foreach (KeyValuePair<string,float> f in k.Value)
            {
                sortedList.Add((k.Key, f.Key, f.Value));
            }
        }

        sortedList = sortedList.OrderBy(entry => entry.Item3).ToList();

        Dictionary<int, Dictionary<string, float>> sortedDictionay = new();

        foreach ((int, string, float) k in sortedList)
        {
            Dictionary<string, float> playerInfo = new();
            playerInfo.Add(k.Item2, k.Item3);
            
            sortedDictionay.Add(k.Item1, playerInfo);
        }

        return sortedDictionay;
    }
    
    public static Dictionary<int, Dictionary<string, float>> GetAllScoreDescending()
    {
        Dictionary<int, Dictionary<string, float>> originDictionary = GetAllScore();
        List<(int, string, float)> sortedList = new();

        foreach (KeyValuePair<int,Dictionary<string,float>> k in originDictionary)
        {
            foreach (KeyValuePair<string,float> f in k.Value)
            {
                sortedList.Add((k.Key, f.Key, f.Value));
            }
        }

        sortedList = sortedList.OrderByDescending(entry => entry.Item3).ToList();

        Dictionary<int, Dictionary<string, float>> sortedDictionay = new();

        foreach ((int, string, float) k in sortedList)
        {
            Dictionary<string, float> playerInfo = new();
            playerInfo.Add(k.Item2, k.Item3);
            
            sortedDictionay.Add(k.Item1, playerInfo);
        }

        return sortedDictionay;
    }
}
