using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] GameObject endPanel;

    [SerializeField] TextMeshProUGUI finalScoreText;


    public void EndGame(int score)
    {
        endPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Game Restarted");
    }

    public void ButtonBackToMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Returned to menu");
    }
}
