using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{

    public string playerName;
    public float score;

    public ScoreHolder(string _name, float _score)
    {
        playerName = _name;
        score = _score;
    }
}
