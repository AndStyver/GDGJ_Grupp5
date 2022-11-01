using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject creditsPanel;
    public void ButtonStart()
    {
        Debug.Log("Game Started");
    }

    public void ButtonTutorial()
    {
        creditsPanel.SetActive(false);
        tutorialPanel.SetActive(!tutorialPanel.activeSelf);
    }

    public void ButtonCredits()
    {
        tutorialPanel.SetActive(false);
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}
