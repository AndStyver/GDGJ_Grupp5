using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardController : MonoBehaviour
{
    SortedDictionary<int, string> scoreboard;

    string names;
    string scores;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //names = gameObject.transform.GetChild(0).GetComponentsInChildren<TMPro>
    }

    void AddNewScore(ScoreHolder score)
    {
        scoreboard.Add(score.score, score.playerName);
    }

    void UpdateScore()
    {
        
    }
}
