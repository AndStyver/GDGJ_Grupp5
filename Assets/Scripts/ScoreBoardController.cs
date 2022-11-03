using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardController : MonoBehaviour
{
    SortedDictionary<int, string> scoreboard;

    public static ScoreBoardController Instance;
    TextMeshProUGUI names;
    TextMeshProUGUI scores;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        names = GameObject.Find("Names").GetComponent<TextMeshProUGUI>();
        scores = GameObject.Find("Scores").GetComponent<TextMeshProUGUI>();
    }

    public void AddNewScore(ScoreHolder score)
    {
        scoreboard.Add(score.score, score.playerName);
    }

    void UpdateScore()
    {
        names.text = "";
        scores.text = "";
        foreach ( var score in scoreboard)
        {
            scores.text += (score.Key + "\n");
            names.text += (score.Value + "\n");
        }
    }
}
