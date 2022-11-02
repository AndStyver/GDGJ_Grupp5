using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] GameObject endPanel;

    [SerializeField] TextMeshProUGUI finalScoreText;

    [SerializeField] PickupController pickups;

    private void Start()
    {
        pickups.GetComponent<PickupController>();
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + pickups.score;
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
