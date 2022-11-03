using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject storyPanel;
    [SerializeField] GameObject scorePanel;

    private void Start()
    {
        ScoreBoardController.instance.setActive(true);
    }
    public void ButtonStart()
    {
        ScoreBoardController.instance.setActive(false);
        SceneManager.LoadScene(1);
        Debug.Log("Game Started");
    }

    public void ButtonTutorial()
    {
        creditsPanel.SetActive(false);
        storyPanel.SetActive(false);
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }

    public void ButtonCredits()
    {
        tutorialPanel.SetActive(false);
        storyPanel.SetActive(false);
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void ButtonStory()
    {
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        storyPanel.SetActive(!storyPanel.activeSelf);
    }

    public void ButtonQuit()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
