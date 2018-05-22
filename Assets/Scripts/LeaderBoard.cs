using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard instance;
    public int entryCount;
    [System.Serializable]
    public struct ScoreEntry
    {
        public string name;
        public int score;
        public ScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
    void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    void Start()
    {
        entryCount = 5;
        LoadScore();
    }
    public List<ScoreEntry> s_Entries;
    public List<ScoreEntry> Entries
    {
        get
        {
            if (s_Entries == null)
            {
                s_Entries = new List<ScoreEntry>();
                // Load Score;

            }
            return s_Entries;
        }
    }
    public void SortScores()
    {
        s_Entries.Sort((a, b) => b.score.CompareTo(a.score));
    }
    public void LoadScore()
    {
        s_Entries.Clear();
        int index = 0;
        for (int i = 0; i < entryCount; ++i)
        {
            ScoreEntry entry;
           
            entry.name = PlayerPrefs.GetString(GameSettings.PlayerPrefsBaseKey + index, "No name");

            entry.score = PlayerPrefs.GetInt(GameSettings.PlayerPrefsBaseKey + index , 0);
            s_Entries.Add(entry);
            index++;
        }

        SortScores();
    }
    private void SaveScores()
    {
        for (int i = 0; i < entryCount; ++i)
        {
            var entry = s_Entries[i];
            Debug.Log("" + entry.name);
            PlayerPrefs.SetString(GameSettings.PlayerPrefsBaseKey + i, entry.name);
            PlayerPrefs.SetInt(GameSettings.PlayerPrefsBaseKey + i, entry.score);
        }
    }
    public ScoreEntry GetEntry(int index)
    {

        return Entries[index];
    }
    public void Record(string name, int score)
    {
        Entries.Add(new ScoreEntry(name, score));
        SortScores();
        Entries.RemoveAt(Entries.Count - 1);
        SaveScores();
    }
    public void Clear()
    {
        for (int i = 0; i < entryCount; ++i)
            s_Entries[i] = new ScoreEntry("", 0);
        SaveScores();
    }
    public bool CheckScore(int currentScore)
    {
        for (int i = 0; i < entryCount; ++i)
        {
            if (Entries[i].score < currentScore)
            {
                return true;
            }
        }
        return false;
    }
}
