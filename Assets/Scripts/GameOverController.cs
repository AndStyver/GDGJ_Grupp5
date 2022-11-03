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

    public EnemyController enemyController;
    public Vector3[] enemySpawnPos;

    public string playerName;


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
      
     public void SpawnGhosts()
    {
        Transform player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        for (var i = 0; i < enemySpawnPos.Length; i++)
        {
            Instantiate(enemyController.gameObject, player.position + enemySpawnPos[i], Quaternion.identity);
        }
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
        
        ScoreHolder newScore = new ScoreHolder(playerName, pickups.score);
        ScoreBoardController.instance.AddNewScore(newScore);
        ScoreBoardController.instance.UpdateScore();
    }

    public void ReadStringInput(string input)
    {
        playerName = input;
        Debug.Log(playerName);
    }
}
