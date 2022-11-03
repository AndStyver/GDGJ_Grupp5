using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{

    public string playerName;
    public int score;

    public ScoreHolder(string _name, int _score)
    {
        playerName = _name;
        score = _score;
    }
}
