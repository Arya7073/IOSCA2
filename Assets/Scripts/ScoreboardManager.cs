using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject scoreboardPanel; // The panel to display the scoreboard
    public TextMeshProUGUI scoreboardText; // Text element to show the leaderboard

    private string saveFilePath;

    private List<ScoreEntry> scores = new List<ScoreEntry>();

    [System.Serializable]
    public class ScoreEntry
    {
        public string playerName;
        public int score;

        public ScoreEntry(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    [System.Serializable]
    public class ScoreData
    {
        public List<ScoreEntry> scores = new List<ScoreEntry>();
    }

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "scoreboard.json");
        LoadScoreboard();
        UpdateScoreboardUI();
    }

    public void AddScore(string playerName, int score)
    {
        // Add a new score entry
        scores.Add(new ScoreEntry(playerName, score));
        // Sort scores in descending order
        scores.Sort((a, b) => b.score.CompareTo(a.score));
        // Save to file
        SaveScoreboard();
        // Update UI
        UpdateScoreboardUI();
    }

    private void SaveScoreboard()
    {
        ScoreData scoreData = new ScoreData { scores = scores };
        string json = JsonUtility.ToJson(scoreData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log($"Scoreboard saved to {saveFilePath}");
    }

    private void LoadScoreboard()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);
            scores = scoreData.scores;
            Debug.Log("Scoreboard loaded.");
        }
        else
        {
            Debug.Log("No existing scoreboard found.");
        }
    }

    private void UpdateScoreboardUI()
    {
        // Clear existing text
        scoreboardText.text = "";
        // Update with the latest scores
        for (int i = 0; i < scores.Count; i++)
        {
            ScoreEntry entry = scores[i];
            scoreboardText.text += $"{i + 1}. {entry.playerName} - {entry.score}\n";
        }
    }

    public void ShowScoreboard()
    {
        scoreboardPanel.SetActive(true);
    }

    public void HideScoreboard()
    {
        scoreboardPanel.SetActive(false);
    }
}
