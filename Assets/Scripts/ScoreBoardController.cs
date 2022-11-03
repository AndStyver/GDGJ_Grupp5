using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    SortedDictionary<int, string> scoreboard;


    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void AddNewScore(ScoreHolder score)
    {
        scoreboard.Add(score.score, score.playerName);
    }
}
