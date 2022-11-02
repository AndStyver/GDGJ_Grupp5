using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupController : MonoBehaviour
{
    [SerializeField] List<GameObject> totalPickups;
    int pickupsLeft;

    public int score;
    [SerializeField] int combo;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

    // Start is called before the first frame update
    void Start()
    {
        totalPickups.AddRange(GameObject.FindGameObjectsWithTag("Pickup"));

        pickupsLeft = totalPickups.Count;

        score = 0;
        ResetCombo();
    }

    public void AddScore(float scoreToAdd)
    {
        score += Mathf.RoundToInt(scoreToAdd * combo);

        combo++;
        //Debug.Log("Score is: " + score + " and Combo is: " + combo);

        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + combo;
    }

    public void ResetCombo()
    {
        combo = 1;
        comboText.text = "Combo: " + combo;
    }

    public void PickupPickedUp()
    {
        score++;
    }
}
