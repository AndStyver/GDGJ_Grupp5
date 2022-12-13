using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardController : MonoBehaviour
{
    public static ScoreBoardController instance;
    public GameObject scorePanel;
    public GameObject gameObjectNames;
    public GameObject gameObjectScores;

    SortedDictionary<float, string> scoreboard;
    TextMeshProUGUI names;
    TextMeshProUGUI scores;

    public ScoreHolder lastAddedScore;

    public void AddNewScore(ScoreHolder score)
    {

        score.score  = -score.score; //invert for sorting

        while (scoreboard.ContainsKey(score.score))
        {
            score.score += 0.0001f;
        }

        instance.lastAddedScore = score;
        Debug.Log("Added last score " + lastAddedScore);
        scoreboard.Add(score.score, score.playerName);
    }

    public void EditLastScoreName(string name)
    {
        if(name == "")
        {
            name = "No Name";
        }
        scoreboard[lastAddedScore.score] = name;
    }

    public void UpdateScore()
    {
        if(names == null)
        {
            names = GameObject.Find("Names").GetComponent<TextMeshProUGUI>();
            scores = GameObject.Find("Scores").GetComponent<TextMeshProUGUI>();
        }

        if (scoreboard == null)
        {
            Debug.Log("UpdateScore: No scoreboard was found");
            return;
        }
        Debug.Log("UpdateScore");


        names.text = "";
        scores.text = "";
        int counter = 0;
        foreach ( var score in scoreboard)
        {
            counter++;
            scores.text += (Mathf.Round(Mathf.Abs(score.Key)) + "\n");
            names.text += (score.Value + "\n");
            Debug.Log(names.text);
            Debug.Log(scores.text);
            if(counter == 5)
            {
                break;
            }
        }
    }

    public bool setActive()
    {
        return scorePanel.activeSelf;
    }

    public void setActive(bool set)
    {
        scorePanel.SetActive(set);
    }

    //Awake is called every time the Menu Scene is loaded 
    void Awake()
    {
        //If we don't have a Scoreboard, make one
        if (instance == null)
        {
            scoreboard = new SortedDictionary<float, string>();
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
