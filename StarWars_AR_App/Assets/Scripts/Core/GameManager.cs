using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string playerName;

    private Dictionary<MovieID, int> scores = new Dictionary<MovieID, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(MovieID movie, int score)
    {
        scores[movie] = score;
        SaveData();
    }

    public int GetScore(MovieID movie)
    {
        return scores.ContainsKey(movie) ? scores[movie] : -1;
    }

    public bool IsCompleted(MovieID movie)
    {
        return scores.ContainsKey(movie);
    }

    void SaveData()
    {
        PlayerPrefs.SetString("PlayerName", playerName);

        foreach (var pair in scores)
        {
            PlayerPrefs.SetInt(pair.Key.ToString(), pair.Value);
        }

        PlayerPrefs.Save();
    }

    void LoadData()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "");

        foreach (MovieID movie in System.Enum.GetValues(typeof(MovieID)))
        {
            if (PlayerPrefs.HasKey(movie.ToString()))
            {
                scores[movie] = PlayerPrefs.GetInt(movie.ToString());
            }
        }
    }
}