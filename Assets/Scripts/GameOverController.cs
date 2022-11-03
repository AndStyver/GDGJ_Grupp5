using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] GameObject endPanel;

    [SerializeField] TextMeshProUGUI finalScoreText;

    [SerializeField] PickupController pickups;

    [SerializeField] Sprite[] gameOverImages;
    [SerializeField] Image gameOverImage;

    private void Start()
    {
        pickups.GetComponent<PickupController>();

        Time.timeScale = 1;
    }

    public void EndGame(bool win)
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + pickups.score;
        if (win) { gameOverImage.sprite = gameOverImages[0]; }
        else { gameOverImage.sprite = gameOverImages[1]; }
    }

    public void ButtonRestart()
    {
        UpdateScore();
        SceneManager.LoadScene(1);
        Debug.Log("Game Restarted");
    }

    public void ButtonBackToMenu()
    {
        UpdateScore();

        SceneManager.LoadScene(0);
        Debug.Log("Returned to menu");
    }

    void UpdateScore()
    {
        string playerName = "Placeholder";
        ScoreHolder newScore = new ScoreHolder(playerName, pickups.score);
        ScoreBoardController.instance.AddNewScore(newScore);
        ScoreBoardController.instance.UpdateScore();
    }
}
