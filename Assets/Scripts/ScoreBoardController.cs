using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreBoardController : MonoBehaviour
{
    public static ScoreBoardController instance;

    SortedDictionary<int, string> scoreboard;
    TextMeshProUGUI names;
    TextMeshProUGUI scores;

    public void AddNewScore(ScoreHolder score)
    {
        score.score  = -score.score; //invert for sorting
        if (score.score == 0)
        {
            Debug.Log("Insufficient Score, no score added");
            return;
        }

        if (score.playerName == "")
        {
            Debug.Log("No name, no score added");
            return;
        }

        if (scoreboard.ContainsKey(score.score))
            scoreboard[score.score] += ", " + score.playerName;
        else
            scoreboard.Add(score.score, score.playerName);
    }

    public void UpdateScore()
    {
        if (scoreboard == null)
        {
            Debug.Log("UpdateScore: No scoreboard was found");
            return;
        }
        Debug.Log("UpdateScore");


        names.text = "";
        scores.text = "";
        foreach ( var score in scoreboard)
        {
            scores.text += (Mathf.Abs(score.Key) + "\n");
            names.text += (score.Value + "\n");
            Debug.Log(names.text);
            Debug.Log(scores.text);
        }
    }

    public bool setActive()
    {
        return transform.GetChild(0).gameObject.activeSelf;
    }

    public void setActive(bool set)
    {
        transform.GetChild(0).gameObject.SetActive(set);
    }

    void Awake()
    {

        if (instance == null)
        {
            scoreboard = new SortedDictionary<int, string>();
            names = GameObject.Find("Names").GetComponent<TextMeshProUGUI>();
            scores = GameObject.Find("Scores").GetComponent<TextMeshProUGUI>();
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
