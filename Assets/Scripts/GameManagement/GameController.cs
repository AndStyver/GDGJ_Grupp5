using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] int maxRooms = 10;
    int roomsLeft;

    [SerializeField] GameObject endPanel;
    [SerializeField] TextMeshProUGUI roomsLeftText;

    private void Start()
    {
        Time.timeScale = 1;
        roomsLeft = maxRooms;

        roomsLeftText.text = "Rooms Left: " + roomsLeft;
    }

    public void NewRoom()
    {
        if (roomsLeft > 0)
        {
            roomsLeft--;
            roomsLeftText.text = "Rooms Left: " + roomsLeft;
        }
        else
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
        endPanel.SetActive(true);
    }
}