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

    GameOverController gameOver;
    ShowPaintingsScript paintings;

    private void Start()
    {
        gameOver = GetComponent<GameOverController>();
        paintings = GetComponent<ShowPaintingsScript>();

        //Enable UICanvas if we forgot
        GameObject.Find("UICanvas").SetActive(true);

        roomsLeft = maxRooms;
        if (roomsLeftText != null)
            roomsLeftText.text = "Rooms Left: " + roomsLeft;
    }

    public void NewRoom()
    {
        if (roomsLeft > 0)
        {
            roomsLeft--;
            if (roomsLeftText != null)
                roomsLeftText.text = "Rooms Left: " + roomsLeft;
            //paintings.ShowNewPaintings();
        }
        else
        {
            gameOver.EndGame(true);
        }
    }
}